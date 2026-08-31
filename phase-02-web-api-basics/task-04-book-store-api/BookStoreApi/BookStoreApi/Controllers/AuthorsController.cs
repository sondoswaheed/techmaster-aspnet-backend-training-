using BookStoreApi.DTOs;
using BookStoreApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var author= _authorService.GetAuthors();
            return Ok(author);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateAuthorRequest request)
        {
            var author = _authorService.Create(request);
            return StatusCode(201,author);
        }
    }
}
