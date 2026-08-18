using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public class ProductQueryService
    {
        private readonly List<Product> _products;

        public ProductQueryService(List<Product> products)
        {
            _products = products;
        }

        public List<Product> GetAllAvailableProducts()
        {
            return _products.Where(p => p.IsAvailable).ToList();
        }

        public List<Product> FilterByCategory(string cat)
        {
            return _products.Where(c => c.Category.Equals(cat, StringComparison.OrdinalIgnoreCase)).ToList();

        }

        public List<Product>? FilterByPriceRange(decimal min, decimal max)
        {
            if (min < 0 || max < 0 || min > max) { return null; }
            return _products.Where(p => p.Price >= min && p.Price <= max).ToList();

        }

        public List<Product>? SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            return _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Product> SortByPrice()
        {
            return _products.OrderBy(p => p.Price).ToList();
        }
        public List<Product> SortByPriceDescending()
        {
            return _products.OrderByDescending(p => p.Price).ToList();
        }
        public IEnumerable<IGrouping<string, Product>> GroupByCategory()
        {
            return _products.GroupBy(c => c.Category).ToList();
        }
        public Dictionary<string, int> CountPerCategory()
        {
            return _products.GroupBy(c => c.Category).ToDictionary(g => g.Key, g => g.Count());
        }
        public decimal CalculateTotalStockValue()
        {
            return _products.Sum(p => p.Price * p.StockQuantity);
        }
        public List<Product> GetTop5MostExpensiveProducts()
        {
            return _products
                .OrderByDescending(p => p.Price)
                .Take(5)
                .ToList();
        }

        public List<Product> GetLowStockProducts()
        {
            return _products
                .Where(p => p.StockQuantity <= 5)
                .ToList();
        }
        public List<Product> GetOutOfStockProducts()
        {
            return _products
                .Where(p => p.StockQuantity == 0 || !p.IsAvailable)
                .ToList();
        }

        public List<ProductSummary> GetProductSummary()
        {
            return _products.Select(p => new ProductSummary
            {
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                StockStatus = p.StockQuantity == 0 ? "Out of stock" : p.StockQuantity <= 5 ? "LowStock" : "In stock"
            }).ToList();
        }

        public List<SupplierReport> GetSupplierReport()
        {
            return _products.GroupBy(p => p.SupplierName).Select(g => new SupplierReport
            {
                SupplierName = g.Key,
                ProductCount = g.Count(),
                StockValue = g.Sum(p => p.Price * p.StockQuantity),
                AveragePrice = g.Average(p => p.Price)
            }).ToList();
        }
        public List<Product> GetRecentlyAddedProducts()
        {
            return _products.Where(p => p.CreatedAt >= DateTime.Today.AddDays(-60)).ToList();
        }

        public List<CategoryStats> GetCategoryStatistics()
        {
            return _products.GroupBy(p => p.Category)
                .Select(g => new CategoryStats
                {
                    Category = g.Key,
                    Count = g.Count(),
                    AveragePrice = g.Average(p => p.Price),
                    MaxPrice = g.Max(p => p.Price),
                    MinPrice = g.Min(p => p.Price),
                    TotalStockValue = g.Sum(p => p.Price * p.StockQuantity)
                })
                .ToList();
        }

        public List<Product> GetProductsAboveAveragePrice()
        {
            var averagePrice = _products.Average(p => p.Price);

            return _products
                .Where(p => p.Price > averagePrice)
                .ToList();
        }

        public List<Product> SearchAndFilter(string category,decimal minPrice,decimal maxPrice,bool isAvailable)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            {
                return new List<Product>();
            }
            // where chain
            return _products.Where(p => p.Category.Equals(category,StringComparison.OrdinalIgnoreCase))
                            .Where(p => p.Price >= minPrice)
                            .Where(p => p.Price <= maxPrice)
                            .Where(p => p.IsAvailable == isAvailable).ToList();
        }

        public List<Product> GetProductsPage(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return new List<Product>();
            }

            return _products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<CategoryStockValue> GetStockValuePerCategory()
        {
            return _products
                .GroupBy(p => p.Category)
                .Select(g => new CategoryStockValue
                {
                    Category = g.Key,
                    StockValue = g.Sum(p => p.Price * p.StockQuantity)
                })
                .ToList();
        }
    }
}