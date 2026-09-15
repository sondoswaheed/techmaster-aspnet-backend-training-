using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_01_ef_core_modeling_drills.DTOs;
using task_01_ef_core_modeling_drills.Interface;

namespace task_01_ef_core_modeling_drills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{id}/tracks")]
        public IActionResult GetbyId(int id)
        {
            var result=_studentService.GetStudentWithTracks(id);
            if (result == null)
                return NotFound(new
                {
                    message = "student not found"
                });
            return Ok(result);
        }

        [HttpGet("isDeleted")]
        public IActionResult GetStudents(bool includeDeleted = false)
        {
            var result = _studentService.GetStudents(includeDeleted);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var result = _studentService.DeleteStudent(id);

            if (!result)
                return NotFound("Student not found");

            return Ok("Student deleted successfully");
        }

        [HttpPost]
        public IActionResult Create(CreateStudentDto dto)
        {
            var result= _studentService.Create(dto);

            if (result == null)
                return NotFound();

            return StatusCode(201,result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateStudentDto dto)
        {
            var result = _studentService.Update(id,dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber <= 0)
                return BadRequest("Page number must be greater than 0.");

            if (pageSize < 1 || pageSize > 50)
                return BadRequest("Page size must be between 1 and 50.");

            var result = await _studentService.GetAll(pageNumber, pageSize);idh

            return Ok(result);
        }

    }
}
