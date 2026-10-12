using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using task_02_requirements_to_erd.Common.Responses;
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

            return Ok(ApiResponse<List<StudentResponse>>.SuccessResponse(result, "students retrieved successfully"));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            var result =_studentService.GetById(id);

            if (result == null)
                return NotFound(ApiResponse<object>.ErrorResponse( "student not found" ));

            return Ok(ApiResponse<StudentResponse>.SuccessResponse(result, "student retrieved successfully"));
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
                    return NotFound(ApiResponse<object>.ErrorResponse("student not found" ));

                return Ok(ApiResponse<StudentResponse>.SuccessResponse(result, "student updated successfully"));
            }catch(InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
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

                return Ok(ApiResponse<StudentResponse>.SuccessResponse(student, "students retrieved successfully"));
            }catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message));
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

                return Ok(ApiResponse<object>.SuccessResponse(student, "enrollments retrieved successfully"));

            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message));
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

                var result = await _studentService.GetMyPayments(userId);

                return Ok(ApiResponse<object>.SuccessResponse(result, "payments retrieved successfully"));

            }catch(UnauthorizedAccessException ex)
            { return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message)); }
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

                return Ok(ApiResponse<StudentResponse>.SuccessResponse(result, "profile updated successfully"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

    }
}
