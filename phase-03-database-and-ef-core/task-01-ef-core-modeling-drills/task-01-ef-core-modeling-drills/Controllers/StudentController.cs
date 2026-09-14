using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
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

    }
}
