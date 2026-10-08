using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_02_requirements_to_erd.DTOs;
using task_02_requirements_to_erd.Interface;

namespace task_02_requirements_to_erd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result =_instructorService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var result=_instructorService.Details(id);

            if(result==null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(int id, UpdateInstructorDto dto)
        {
            var result = _instructorService.Update(id,dto);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateInstructorDto dto)
        {
            var result = _instructorService.Create(dto);

            return StatusCode(201,result);
        }

        [HttpGet("{id}/tracks")]
        public IActionResult GetInstructorsWithTracks(int id)
        {
            var result = _instructorService.GetInstructorWithTracks(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

    }
}
