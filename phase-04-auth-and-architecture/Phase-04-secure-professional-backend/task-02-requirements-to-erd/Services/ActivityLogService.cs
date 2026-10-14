
using System.Security.Claims;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using task_02_requirements_to_erd.Data;
using task_02_requirements_to_erd.DTOs.ActivityLogs;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.Services;

public class ActivityLogService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ActivityLogService( AppDbContext context,  IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(string action, string entityName, string? entityId, string description, object? metadata = null,
        string? actorUserId = null, string? actorRole = null)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var userId = actorUserId ?? httpContext?.User.FindFirstValue( ClaimTypes.NameIdentifier);

        var role = actorRole ?? httpContext?.User.FindFirstValue( ClaimTypes.Role) ?? "Unknown";

        var log = new ActivityLog
        {
            UserId = userId,
            UserRole = role,
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            Description = description,
            CreatedAt = DateTime.UtcNow,
            IpAddress = httpContext?.Connection.RemoteIpAddress?.ToString(),
            RequestId = httpContext?.TraceIdentifier,
            Metadata = metadata == null ? null : JsonSerializer.Serialize(metadata)
        };

        _context.ActivityLogs.Add(log);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<ActivityLogResponse>> GetLogsAsync( int pageNumber = 1, int pageSize = 10, string? userId = null,
        string? entityName = null, DateTime? from = null, DateTime? to = null)
    {
        pageNumber = Math.Max(pageNumber, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _context.ActivityLogs.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(userId))
            query = query.Where(x => x.UserId == userId);

        if (!string.IsNullOrWhiteSpace(entityName))
            query = query.Where(x =>
                x.EntityName == entityName);

        if (from.HasValue)
            query = query.Where(x => x.CreatedAt >= from.Value);

        if (to.HasValue)
            query = query.Where(x => x.CreatedAt <= to.Value);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ActivityLogResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                UserRole = x.UserRole,
                Action = x.Action,
                EntityName = x.EntityName,
                EntityId = x.EntityId,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                IpAddress = x.IpAddress,
                Metadata = x.Metadata,
                RequestId = x.RequestId
            })
            .ToListAsync();

        return new PagedResult<ActivityLogResponse>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
