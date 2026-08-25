using ApiRoutingDrills.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return StatusCode(200, notes);
        } 
    }
}
