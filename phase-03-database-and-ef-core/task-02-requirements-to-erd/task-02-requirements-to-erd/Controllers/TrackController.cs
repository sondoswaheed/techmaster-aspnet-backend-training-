using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models.Enums;

[Route("api/[controller]")]
[ApiController]
public class TracksController : ControllerBase
{
    private readonly ITrainingService _trainingService;

    public TracksController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpGet]
    public IActionResult GetAll(string? keyword,int? level, EnrollmentStatus? status, int? instructorId)
    {
        var result = _trainingService.GetAll( keyword,level, status, instructorId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Details(int id)
    {
        var result = _trainingService.Details(id);

        if (result == null)
            return NotFound(new
            {
                message = "Training track not found."
            });

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create([FromForm]CreateTrackDto dto)
    {
        try
        {
            var result = _trainingService.Create(dto);

            return StatusCode(201, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
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

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
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

            return Ok(new
            {
                message = "Training track deleted successfully."
            });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }
}