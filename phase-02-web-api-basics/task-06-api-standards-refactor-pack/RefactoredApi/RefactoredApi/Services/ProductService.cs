using RefactoredApi.DTOs;
using RefactoredApi.Models;

namespace RefactoredApi.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> products=new List<Product>();

        public ProductResponse Add(CreateProductRequest request)
        {
            var product = new Product
            {
                Id = products.Count + 1,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            products.Add(product);
            return MapToResponse(product);
        }

        public List<ProductResponse> GetAll()
        {
            return products.Select(MapToResponse).ToList();
        }

        public ProductResponse GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            return MapToResponse(product);
        }


        //helper method
        private ProductResponse MapToResponse(Product product)
        {
            return new ProductResponse
            {
                Id= product.Id,
                Name= product.Name,
                Price= product.Price,
                Stock= product.Stock
            };
        }
    }
}
