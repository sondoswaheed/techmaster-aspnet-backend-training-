using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_02_requirements_to_erd.Common.Responses;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll(EnrollmentStatus? status,int? trackId, int? studentId, PaymentStatus? paymentStatus)
        {
            var enrollments = _enrollmentService.GetAll( status,trackId, studentId, paymentStatus);

            return Ok(ApiResponse<List<EnrollmentResponseDto>>.SuccessResponse(enrollments, "enrollments retrieved successfully"));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            var enrollment = _enrollmentService.Details(id);

            if (enrollment == null)
            {
                return NotFound("Enrollment doesn't exist");
            }

            return Ok(ApiResponse<EnrollmentResponseDto>.SuccessResponse(enrollment, "enrollment retrieved successfully"));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromForm]CreateEnrollmentDto dto)
        {
            try
            {
                var enrollment = _enrollmentService.Create(dto);

                return StatusCode(201, ApiResponse<object>.SuccessResponse(enrollment, "Enrollment Created successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateStatus( int id, EnrollmentStatus status)
        {
            try
            {
                var enrollment = _enrollmentService.UpdateStatus(id, status);

                if (enrollment == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResponse("Enrollment doesn't exist"));
                }

                return Ok(ApiResponse<object>.SuccessResponse(enrollment, "status updated successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("/api/students/{id}/enrollments")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetStudentEnrollments(int id)
        {
            try
            {
                var enrollments = _enrollmentService.GetStudentById(id);

                return Ok(ApiResponse<object>.SuccessResponse(enrollments, "enrollments retrieved successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpGet("/api/tracks/{id}/students")]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult GetTrackStudents(int id)
        {
            try
            {
                var students = _enrollmentService.GetTracksById(id);

                return Ok(ApiResponse<List<StudentResponse>>.SuccessResponse(students, "Tracks retreived successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("enrollment-requests")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> RequestEnrollment( EnrollmentRequestDto request)
        {
            var userId = User.FindFirstValue( ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var result = await _enrollmentService .RequestEnrollmentAsync(userId, request.TrainingTrackId);

                return StatusCode( 201, ApiResponse<object>.SuccessResponse(result, "enrollment requested successfully"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }

}