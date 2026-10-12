using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_02_requirements_to_erd.Common.Responses;
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

            return Ok(ApiResponse<List<InstructorResponse>>.SuccessResponse(
            result,
            "Instructors retrieved successfully."
        ));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var result = _instructorService.Details(id);
            if (result == null)
            {
                return NotFound(
                    ApiResponse<object>.ErrorResponse("Instructor not found.")
                );
            }

            return Ok( ApiResponse<InstructorResponse>.SuccessResponse( result, "Instructor retrieved successfully."));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] UpdateInstructorDto dto)
        {
            try
            {
                var result = _instructorService.Update(id, dto);

                if (result == null)
                {
                    return NotFound(
                        ApiResponse<object>.ErrorResponse("Instructor not found.")
                    );
                }

                return Ok(
                    ApiResponse<InstructorResponse>.SuccessResponse(
                        result,
                        "Instructor updated successfully."
                    )
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    ApiResponse<object>.ErrorResponse(ex.Message)
                );
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateInstructorDto dto)
        {
            try
            {
                var result = await _instructorService.CreateAsync(dto);


                return StatusCode(
                    201,
                    ApiResponse<InstructorResponse>.SuccessResponse(
                        result, "Instructor created successfully."
                    )
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    ApiResponse<object>.ErrorResponse(ex.Message)
                );
            }
        }

        [HttpGet("{id}/tracks")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInstructorsWithTracks(int id)
        {
            var result = _instructorService.GetInstructorWithTracks(id);

            if (result == null)
                return NotFound( ApiResponse<object>.ErrorResponse("Instructor not found."));

            return Ok(ApiResponse<InstructorTracksDto>.SuccessResponse(
            result,
            "Instructor tracks retrieved successfully."
        ));
        }

        [HttpPut("me")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateMyInstructorProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(
                    ApiResponse<object>.ErrorResponse("Authentication required.")
                );
            }

            try
            {
                var result = await _instructorService.UpdateMyProfileAsync(userId, dto);

                return Ok(
                    ApiResponse<InstructorResponse>.SuccessResponse(
                        result,
                        "Profile updated successfully."
                    )
                );
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(
                    ApiResponse<object>.ErrorResponse(ex.Message)
                );
            }
        }


        private string? CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet("my-tracks")]
        [Authorize(Roles ="Instructor")]
        public async Task<IActionResult> GetMyTracks()
        {
            if (CurrentUserId == null)
                return Unauthorized();

            var result = await _instructorService.GetMyTracksAsync(CurrentUserId);

            return Ok(ApiResponse<object>.SuccessResponse(result,"Tracks retreived successfully"));
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

                return Ok(ApiResponse<object>.SuccessResponse(result, "Students retrieved successfully"));
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
                var result = await _instructorService.CreateSessionAsync(CurrentUserId, id, dto);

                return StatusCode(201, ApiResponse<object>.SuccessResponse(result, "Session Created successfully"));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
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

                return Ok(ApiResponse<object>.SuccessResponse(result, "Session Updated successfully"));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
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
                var result = await _instructorService.GetTrackProgressAsync(CurrentUserId, id);

                return Ok(ApiResponse<object>.SuccessResponse(result));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }

        }
    }
}
