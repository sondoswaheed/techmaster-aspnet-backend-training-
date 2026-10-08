using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public IActionResult GetAll(EnrollmentStatus? status,int? trackId, int? studentId, PaymentStatus? paymentStatus)
        {
            var enrollments = _enrollmentService.GetAll( status,trackId, studentId, paymentStatus);

            return Ok(enrollments);
        }

        [HttpGet("{id}")]
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
    }
}