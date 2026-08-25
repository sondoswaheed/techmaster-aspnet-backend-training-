using ApiRoutingDrills.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static readonly List<NoteResponse> Notes = new();

        [HttpPost]
        public IActionResult Create([FromBody] CreateNoteRequest request)
        {
            var notes = new NoteResponse
            {
                Id = Notes.Count + 1,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.Now
            };

            Notes.Add(notes);
            return StatusCode(201, notes);
        } 

        [HttpGet]
        //pagination
        public IActionResult GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0)
            {
                return BadRequest(new
                {
                    message = "Page number must be greater than 0"
                });
            }

            if (pageSize < 1 || pageSize > 50)
            {
                return BadRequest(new
                {
                    message = "Page size must be between 1 and 50"
                });
            }

            var totalCount = Notes.Count;

            var items = Notes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                items = items,
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalCount = totalCount
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var note= Notes.FirstOrDefault(i=>i.Id==id);
            if(note == null)
            {
               return NotFound(new {message="Note not found"});
            }
            return Ok(note);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id , [FromBody] UpdateNoteRequest request)
        {
           var note =Notes.FirstOrDefault(i=>i.Id==id);
            // not found 404 
            if (note == null) { return NotFound(new { message = "Id doesn't exist" }); }

            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Content)) {
                // status code 400
                return BadRequest();
            }
            note.Title = request.Title;
            note.Content = request.Content;

            return Ok(
                new { Updatednotes=note,
                message="updated note returned"
            });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = Notes.FirstOrDefault(x=>x.Id==id);
            if (note == null)
            {
                return NotFound();
            }
            Notes.Remove(note);
            return NoContent();
        }

        [HttpGet("Search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Keyword is required"
                });
            }

            var matchingNotes = Notes.Where(note =>note.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                             note.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(matchingNotes);
        }
        
    }
}
