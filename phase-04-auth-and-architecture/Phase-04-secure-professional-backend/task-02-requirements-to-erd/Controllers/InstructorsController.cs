using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var result = _instructorService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var result = _instructorService.Details(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, UpdateInstructorDto dto)
        {
            var result = _instructorService.Update(id, dto);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateInstructorDto dto)
        {
            try
            {
                var result = await _instructorService.CreateAsync(dto);

                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/tracks")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInstructorsWithTracks(int id)
        {
            var result = _instructorService.GetInstructorWithTracks(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut("me")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> UpdateMyProfile(
    UpdateMyInstructorProfileDto dto)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var result = await _instructorService
                    .UpdateMyProfileAsync(userId, dto);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        private string? CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet("my-tracks")]
        [Authorize(Roles ="Instructor")]
        public async Task<IActionResult> GetMyTracks()
        {
            if (CurrentUserId == null)
                return Unauthorized();

            var result = await _instructorService
                .GetMyTracksAsync(CurrentUserId);

            return Ok(result);
        }

        [HttpGet("tracks/{id:int}/students")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> GetTrackStudents(int id)
        {
            if (CurrentUserId == null)
                return Unauthorized();

            try
            {
                var result = await _instructorService.GetTrackStudentsAsync(CurrentUserId, id);

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("tracks/{id:int}/sessions")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> CreateSession(int id,CreateTrackSessionDto dto)
        {
            if (CurrentUserId == null)
                return Unauthorized();

            try
            {
                var result = await _instructorService
                    .CreateSessionAsync(CurrentUserId, id, dto);

                return StatusCode(201, result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("sessions/{id:int}")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> UpdateSession( int id,  UpdateTrackSessionDto dto)
        {
            if (CurrentUserId == null)
                return Unauthorized();

            try
            {
                var result = await _instructorService.UpdateSessionAsync(CurrentUserId, id, dto);

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpGet("tracks/{id:int}/progress")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> GetTrackProgress(int id)
        {
            if (CurrentUserId == null)
                return Unauthorized();

            try
            {
                var result = await _instructorService
                    .GetTrackProgressAsync(CurrentUserId, id);

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
    }
}
