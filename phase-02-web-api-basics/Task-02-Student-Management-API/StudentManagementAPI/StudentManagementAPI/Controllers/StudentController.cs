using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Interfaces;

namespace StudentManagementAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? search, [FromQuery] string? trackName, [FromQuery] bool? isActive)
        {
            var students = _studentService.GetAll(search, trackName, isActive);
            return Ok(students);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateStudentRequest createDto)
        {
            try
            {
                var student = _studentService.CreateStudent(createDto);

                return StatusCode(201, student);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student not found"
                });
            }

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateStudentRequest updateDto)
        {
            try
            {
                var student = _studentService.UpdateStudent(id, updateDto);

                if (student == null)
                {
                    return NotFound(new
                    {
                        message = "Student not found"
                    });
                }

                return Ok(student);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPatch("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] UpdateStudentStatusRequest update)
        {
            var student = _studentService.UpdateStudentStatus(id, update);

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student not found"
                });
            }

            return Ok(new
            {
                message = "Student status updated successfully",
                studentId = student.StudentId,
                isActive = student.IsActive
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _studentService.DeleteStudent(id);
            if (!isDeleted) return NotFound(new { message = "Student not found" });

            return NoContent();
        }

        [HttpGet("by-track/{trackName}")]
        public IActionResult GetByTrack(string trackName)
        {
            var result = _studentService.GetByTrack(trackName);
            return Ok(result);
        }

        [HttpGet("stats")]
        public IActionResult StudentsStats()
        {
            var stats = _studentService.GetStudentStats();
            return Ok(stats);
        }
    }
}