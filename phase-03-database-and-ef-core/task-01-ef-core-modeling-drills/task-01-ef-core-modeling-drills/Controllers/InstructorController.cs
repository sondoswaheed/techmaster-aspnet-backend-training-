using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_01_ef_core_modeling_drills.Interface;

namespace task_01_ef_core_modeling_drills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet("instructors/{id}/tracks")]
        public IActionResult GetInstructorById(int id)
        {
            var instructor = _instructorService.GeInstructorWithTrack(id);
            if (instructor == null)
            {
                return NotFound(new
                {
                    message = "Instructor not found"
                });
            }

            return Ok(instructor);
        }
    }
}
