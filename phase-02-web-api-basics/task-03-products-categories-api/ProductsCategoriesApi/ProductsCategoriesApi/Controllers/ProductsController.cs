
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

        [HttpGet]
        public IActionResult GetProducts(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? lowStock)
        {
            var product = _productService.SearchByName(name, categoryId, minPrice, maxPrice, isAvailable, lowStock);
            return Ok(product);
        }

        [HttpGet("reports/stock-value")]
        public IActionResult StockReports()
        {
            var reports = _productService.StockReport();
            return Ok(reports);
        }

        [HttpPatch("{id}/stock")]
        public IActionResult UpdateStockQuantiy(int id, UpdateStockRequest request)
        {
            var stock = _productService.UpdateStock(id, request);
            if (stock == null)
            {
                return NotFound(new
                {
                    message="product not found"
                });
            }
            return Ok(stock);
        }

        [HttpGet("low-stock")]
        public IActionResult LowStock()
        {
            var stock=_productService.LowStock();
            
            return Ok(stock);
        }
    }
}
