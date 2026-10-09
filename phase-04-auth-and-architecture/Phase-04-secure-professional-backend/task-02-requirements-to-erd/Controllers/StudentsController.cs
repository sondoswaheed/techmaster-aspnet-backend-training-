using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll(int pageSize, int pageNumber, bool? IsActive, string? search)
        {
            var result = _studentService.GetAll( pageSize,pageNumber, IsActive, search);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            var result =_studentService.GetById(id);

            if (result == null)
                return NotFound(new { message = "student not found" });

            return Ok(result);
        }

        //[HttpPost]
        //public IActionResult Create(CreateStudentDto dto)
        //{
        //    try
        //    {
        //        var result = _studentService.Create(dto);

        //        return StatusCode(201, result);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id , UpdateStudentDto dto)
        {
            try
            {
                var result = _studentService.Update(id, dto);

                if (result == null)
                    return NotFound(new { message = "student not found" });

                return Ok(result);
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _studentService.Delete(id);

            if (!result)
                return NotFound(new { message = "student not found" });

            return Ok(new { message = "student deleted successfully" });
        }

        [HttpGet("me")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var student = await _studentService.GetMyProfileAsync(userId);

                if (student == null)
                    return NotFound("Student not found ");

                return Ok(student);
            }catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("my-Enrollments")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyEnrollments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
                return Unauthorized();
            try
            {
                var student = await _studentService.GetMyEnrollmentAsync(userId);

                if (student == null)
                    return NotFound("Student not found ");

                return Ok(student);

            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("my-payments")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyPayments()
        {
            try
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var result = await _studentService
                    .GetMyPayments(userId);

                return Ok(result);
            }catch(UnauthorizedAccessException ex)
            { return Unauthorized(ex.Message); }
        }


        [HttpPut("me")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> UpdateMyProfile(UpdateStudentDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(userId))
                return Unauthorized();
            try
            {
                var result = await _studentService
                    .UpdateMyProfileAsync(userId, request);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
