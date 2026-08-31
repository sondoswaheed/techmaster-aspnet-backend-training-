using BookStoreApi.DTOs;
using BookStoreApi.Interfaces;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateCategoryRequest request)
        {
            try
            {
                var category = _categoryService.Create(request);
                return StatusCode(201, category);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var category= _categoryService.GetAll();
            return Ok(category);
        }
    }
}
