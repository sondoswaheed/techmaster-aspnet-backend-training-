using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactoredApi.DTOs;
using RefactoredApi.Services;

namespace RefactoredApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Create(CreateProductRequest request)
        {
            var product = _productService.Add(request);

            return StatusCode(201,product);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _productService.GetAll();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product =_productService.GetById(id);

            if (product == null)
            {
                return NotFound("the product doesn't exist");
            }

            return Ok(product);
        }
    }
    
}