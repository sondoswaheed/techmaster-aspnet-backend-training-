using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAll(int pageSize, int pageNumber, bool? IsActive, string? search)
        {
            var result = _studentService.GetAll( pageSize,pageNumber, IsActive, search);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result =_studentService.GetById(id);

            if (result == null)
                return NotFound(new { message = "student not found" });

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateStudentDto dto)
        {
            try
            {
                var result = _studentService.Create(dto);

                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
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
        public IActionResult Delete(int id)
        {
            var result = _studentService.Delete(id);

            if (!result)
                return NotFound(new { message = "student not found" });

            return Ok(new { message = "student deleted successfully" });
        }

    }
}
