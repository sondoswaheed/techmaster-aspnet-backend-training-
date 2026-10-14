
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.Common.Responses;
using task_02_requirements_to_erd.DTOs.ActivityLogs;
using task_02_requirements_to_erd.Services;

namespace task_02_requirements_to_erd.Controllers;

[ApiController]
[Route("api/admin/activity-logs")]
[Authorize(Roles = "Admin")]
public class ActivityLogsController : ControllerBase
{
    private readonly ActivityLogService _activityLogService;

    public ActivityLogsController( ActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ActivityLogResponse>>>>
        GetLogs(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? userId = null,
            [FromQuery] string? entityName = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
    {
        if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
        {
            return BadRequest( ApiResponse<object>.ErrorResponse("Invalid pagination values.",
                    new List<string>
                    {
                        "pageNumber must be at least 1.",
                        "pageSize must be between 1 and 100."
                    }));
        }

        if (from.HasValue && to.HasValue && from.Value > to.Value)
        {
            return BadRequest( ApiResponse<object>.ErrorResponse("The from date cannot be later than the to date."));
        }

        var result = await _activityLogService.GetLogsAsync( pageNumber, pageSize, userId, entityName, from, to);

        return Ok(
            ApiResponse<PagedResult<ActivityLogResponse>>.SuccessResponse( result,"Activity logs retrieved successfully."));
    }
}
