
namespace task_02_requirements_to_erd.DTOs.ActivityLogs;

public class ActivityLogResponse
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string UserRole { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? IpAddress { get; set; }
    public string? Metadata { get; set; }
    public string? RequestId { get; set; }
}
