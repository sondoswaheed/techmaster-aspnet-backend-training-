using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.Common.Responses;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models.Enums;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TracksController : ControllerBase
{
    private readonly ITrainingService _trainingService;

    public TracksController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Student")]
    public IActionResult GetAll(string? keyword,int? level, EnrollmentStatus? status, int? instructorId)
    {
        var result = _trainingService.GetAll( keyword,level, status, instructorId);

        return Ok(ApiResponse<object>.SuccessResponse(result, "Tracks retrieved successfully"));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Details(int id)
    {
        var result = _trainingService.Details(id);

        if (result == null)
            return NotFound(new
            {
                message = "Training track not found."
            });

        return Ok(ApiResponse<object>.SuccessResponse(result, "Track details retreived successfully"));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([FromForm]CreateTrackDto dto)
    {
        try
        {
            var result = _trainingService.Create(dto);

            return StatusCode(201, ApiResponse<TrainingTrackResponse>.SuccessResponse(result, "Tracks created successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(int id,[FromForm] UpdateTrackDto dto)
    {
        try
        {
            var result = _trainingService.Update(id, dto);

            if (result == null)
                return NotFound(new
                {
                    message = "Training track not found."
                });

            return Ok(ApiResponse<TrainingTrackResponse>.SuccessResponse(result, "Tracks updated successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        try
        {
            var result = _trainingService.Delete(id);

            if (!result)
                return NotFound(new
                {
                    message = "Training track not found."
                });

            return Ok(ApiResponse<object>.SuccessResponse(result, "Tracks deleted successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }
}