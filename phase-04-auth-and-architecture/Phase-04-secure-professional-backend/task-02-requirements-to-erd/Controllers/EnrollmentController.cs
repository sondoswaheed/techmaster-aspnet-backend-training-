using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
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

            return Ok(enrollments);
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

            return Ok(enrollment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromForm]CreateEnrollmentDto dto)
        {
            try
            {
                var enrollment = _enrollmentService.Create(dto);

                return StatusCode(201, enrollment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
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
                    return NotFound("Enrollment doesn't exist");
                }

                return Ok(enrollment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/students/{id}/enrollments")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetStudentEnrollments(int id)
        {
            try
            {
                var enrollments = _enrollmentService.GetStudentById(id);

                return Ok(enrollments);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/api/tracks/{id}/students")]
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult GetTrackStudents(int id)
        {
            try
            {
                var students = _enrollmentService.GetTracksById(id);

                return Ok(students);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
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

                return StatusCode( 201, result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }

}