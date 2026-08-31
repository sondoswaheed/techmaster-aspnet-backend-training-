using BookStoreApi.DTOs;
using BookStoreApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult Create(CreateBookRequest request)
        {
            try
            {
                var book = _bookService.Create(request);

                return StatusCode(201, book);
            }catch(InvalidOperationException ex)
            {
                return Conflict(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateBookRequest request)
        {
            try
            {
                var book = _bookService.Update(id, request);

                if (book == null)
                {
                    return NotFound("the book isn't exist");
                }

                return Ok(book);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message=ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book=_bookService.GetById(id);

            if(book == null)
            {
                return NotFound("the book isn't exist");
            }
            return Ok(book);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book= _bookService.Delete(id);

            if (!book)
            {
                return NotFound("the book isn't exist");
            }
            return Ok( new
            {
                message = "Book marked as unavailable successfully."
            });
        }

        [HttpGet("reports/summary")]
        public IActionResult GetSummary()
        {
            var summary = _bookService.GetReports();

            return Ok(summary);
        }

        [HttpGet]

        public IActionResult GetWithSearch([FromQuery] BookSearchRequest request)
        {
            try
            {
                var search = _bookService.GetAll(request);

                return Ok(search);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(
                    new
                    {
                        message = ex.Message
                    });
            }
        }
    }
}
