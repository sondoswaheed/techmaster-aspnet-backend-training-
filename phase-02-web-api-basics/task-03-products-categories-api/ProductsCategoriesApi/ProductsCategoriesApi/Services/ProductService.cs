using Microsoft.AspNetCore.Http.HttpResults;
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

            products.AddRange(new List<Product>
    {
        // Electronics
        new Product
        {
            ProductId = 1,
            Name = "Laptop",
            CategoryId = 1,
            Price = 25000,
            StockQuantity = 3,
            IsAvailable = true,
            SupplierName = "Dell",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 2,
            Name = "Mouse",
            CategoryId = 1,
            Price = 500,
            StockQuantity = 10,
            IsAvailable = true,
            SupplierName = "Logitech",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 3,
            Name = "Keyboard",
            CategoryId = 120,
            Price = 1200,
            StockQuantity = 4,
            IsAvailable = true,
            SupplierName = "Redragon",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 4,
            Name = "Monitor",
            CategoryId = 1,
            Price = 7000,
            StockQuantity = 0,
            IsAvailable = false,
            SupplierName = "Samsung",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 5,
            Name = "USB-C Hub",
            CategoryId = 1,
            Price = 1500,
            StockQuantity = 7,
            IsAvailable = true,
            SupplierName = "Anker",
            CreatedAt = DateTime.Now
        },

        // Furniture
        new Product
        {
            ProductId = 6,
            Name = "Office Chair",
            CategoryId = 2,
            Price = 4500,
            StockQuantity = 6,
            IsAvailable = true,
            SupplierName = "IKEA",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 7,
            Name = "Desk",
            CategoryId = 2,
            Price = 6500,
            StockQuantity = 2,
            IsAvailable = true,
            SupplierName = "IKEA",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 8,
            Name = "Desk Lamp",
            CategoryId = 2,
            Price = 900,
            StockQuantity = 12,
            IsAvailable = true,
            SupplierName = "Philips",
            CreatedAt = DateTime.Now
        },

        // Stationery
        new Product
        {
            ProductId = 9,
            Name = "Notebook",
            CategoryId = 3,
            Price = 100,
            StockQuantity = 20,
            IsAvailable = true,
            SupplierName = "PaperLine",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 10,
            Name = "Pen Set",
            CategoryId = 3,
            Price = 150,
            StockQuantity = 3,
            IsAvailable = true,
            SupplierName = "BIC",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 11,
            Name = "Marker",
            CategoryId = 3,
            Price = 80,
            StockQuantity = 8,
            IsAvailable = true,
            SupplierName = "Faber-Castell",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 12,
            Name = "Paper Pack",
            CategoryId = 3,
            Price = 250,
            StockQuantity = 0,
            IsAvailable = false,
            SupplierName = "Double A",
            CreatedAt = DateTime.Now
        },

        // Accessories
        new Product
        {
            ProductId = 13,
            Name = "Backpack",
            CategoryId = 4,
            Price = 1200,
            StockQuantity = 5,
            IsAvailable = true,
            SupplierName = "Samsonite",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 14,
            Name = "Mouse Pad",
            CategoryId = 4,
            Price = 300,
            StockQuantity = 15,
            IsAvailable = true,
            SupplierName = "Razer",
            CreatedAt = DateTime.Now
        },

        new Product
        {
            ProductId = 15,
            Name = "Laptop Sleeve",
            CategoryId = 4,
            Price = 800,
            StockQuantity = 2,
            IsAvailable = true,
            SupplierName = "Targus",
            CreatedAt = DateTime.Now
        }
    });
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

        public List<ProductResponse> SearchByName(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? lowStock)
        {


            var product = products.Where(d => string.IsNullOrEmpty(name) || d.Name.Contains(name, StringComparison.OrdinalIgnoreCase));


            if (categoryId.HasValue)
            {
                product = product.Where(k => k.CategoryId == categoryId.Value);
            }
            if (maxPrice.HasValue)
            {
                product = product.Where(s => s.Price <= maxPrice.Value);
            }
            if (minPrice.HasValue)
            {
                product = product.Where(s => s.Price >= minPrice.Value);
            }
            if (isAvailable.HasValue)
            {
                product = product.Where(s => s.IsAvailable == isAvailable.Value);
            }
            if (lowStock.HasValue)
            {
                if (lowStock.Value)
                {
                    product = product.Where(s => s.StockQuantity <= 5);
                }
                else
                {
                    product = product.Where(s => s.StockQuantity > 5);
                }
            }

                    return product.Select(MapToResponse).ToList();

        }

        public StockReportResponse StockReport()
        {
            var totalStock=products.Sum(s=>s.StockQuantity * s.Price);

            var stockValuePerCategory = products.GroupBy(s => s.CategoryId).ToDictionary(
                k => k.Key.ToString(),
                k => k.Sum(o => o.Price * o.StockQuantity));

            var lowStock = products.Where(s => s.StockQuantity <= 5).Select(MapToResponse).ToList();

            var outOfStock = products.Where(s => s.StockQuantity == 0).Select(MapToResponse).ToList();

            var productsCount = products.GroupBy(d => d.CategoryId).ToDictionary(
                d => d.Key.ToString(),
                d => d.Count());

            return new StockReportResponse
            {
                TotalStockValue = totalStock,
                OutOfStockProducts = outOfStock,
                LowStockProducts = lowStock,
                ProductsCountByCategory = productsCount,
                StockValuePerCategory = stockValuePerCategory
            };
        }

        public ProductResponse UpdateStock(int id, UpdateStockRequest request)
        {
            var product = products.FirstOrDefault(s => s.ProductId == id);
            if (product == null)
            {
                return null;
            }
            product.StockQuantity= request.StockQuantity;
            return MapToResponse(product);
        }

        public List<ProductResponse> LowStock()
        {
            var product = products.Where(s => s.StockQuantity <= 5).Select(MapToResponse).ToList();
            return product;
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
