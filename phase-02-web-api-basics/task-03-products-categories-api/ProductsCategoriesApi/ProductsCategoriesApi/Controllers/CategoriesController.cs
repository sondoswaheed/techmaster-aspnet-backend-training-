using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;
using ProductsCategoriesApi.Services;

namespace ProductsCategoriesApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateCategoryRequest request)
        {
            var category= _categoryService.CreateCategory(request);
            return StatusCode(201, category);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

    }
}
