using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> products=new List<Product>();

        private readonly ICategoryService _categoryService;

        public ProductService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ProductResponse Create(CreateProductRequest request)
        {
            var CategoryExist = _categoryService.CategoryExists(request.CategoryId);
            if (!CategoryExist)
            {
                throw new InvalidOperationException("Category not Found");
            }
            var product = new Product
            {
                ProductId=products.Count+1,
                Name = request.Name,
                SupplierName = request.SupplierName,
                CategoryId = request.CategoryId,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            };
            products.Add(product);
            return MapToResponse(product);
           
        }

        public List<ProductResponse> GetAll()
        {
           return products.Select(MapToResponse).ToList();
        }

        public ProductResponse Update(int id ,UpdateProductRequest request)
        {
            var product =products.FirstOrDefault(s=>s.ProductId== id);

            if (product == null)
            {
                return null;
            }

            var CategoryExist = _categoryService.CategoryExists(request.CategoryId);
            if (!CategoryExist)
            {
                throw new InvalidOperationException("Category not Found");
            }
            product.Name= request.Name;
            product.CategoryId= request.CategoryId;
            product.Price= request.Price;
            product.StockQuantity= request.StockQuantity;
            product.IsAvailable= request.IsAvailable;
            product.SupplierName= request.SupplierName;

            return MapToResponse(product);

        }

        public ProductResponse GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return null;
            }

            return MapToResponse(product);
        }

        public bool Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return false;
            }
            // make product unvisible
            product.IsAvailable = false;

            return true;
        }





        public ProductResponse MapToResponse(Product product)
        {
            return new ProductResponse
            {
                ProductId= product.ProductId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                SupplierName = product.SupplierName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsAvailable = product.IsAvailable,
                CreatedAt = product.CreatedAt,

            };
        }
    }
}
