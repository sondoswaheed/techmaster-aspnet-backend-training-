using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Services;

namespace ProductsCategoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            try
            {
                var product = _productService.Create(request);
                return StatusCode(201, product);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id , [FromBody]UpdateProductRequest request)
        {
            try
            {
                var product = _productService.Update(id, request);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Product not found"
                });
            }

            return Ok(product);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var deleted = _productService.Delete(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Product not found"
                });
            }

            return Ok(new
            {
                message = "Product marked as unavailable successfully"
            });
        }
    }
}
