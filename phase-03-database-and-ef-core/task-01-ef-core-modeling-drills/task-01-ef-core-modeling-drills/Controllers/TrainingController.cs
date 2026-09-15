using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_01_ef_core_modeling_drills.Interface;

namespace task_01_ef_core_modeling_drills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrackService _trackService;
        public TrainingController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet("{id}/students")]
        public IActionResult GetTracksById(int id)
        {
            var result =_trackService.GettrackWithStudents(id);

            if (result == null)
                return NotFound(new { message = "Track is not found" });

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetTracks()
        {
            var result = _trackService.Details();
            return Ok(result);
        }
    }
}
