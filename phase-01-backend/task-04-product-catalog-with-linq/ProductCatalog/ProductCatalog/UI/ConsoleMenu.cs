using ProductCatalog.Models;
using ProductCatalog.Services;

namespace ProductCatalog.UI
{
    public class ConsoleMenu
    {
        private readonly ProductQueryService _productQueryService;

        public ConsoleMenu(ProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                PrintMenu();

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowAvailableProducts();
                        break;

                    case "2":
                        FilterByCategory();
                        break;

                    case "3":
                        FilterByPriceRange();
                        break;

                    case "4":
                        SearchByName();
                        break;

                    case "5":
                        ShowProducts(_productQueryService.SortByPrice());
                        break;

                    case "6":
                        ShowProducts(_productQueryService.SortByPriceDescending());
                        break;

                    case "7":
                        GroupProductsByCategory();
                        break;

                    case "8":
                        CountProductsPerCategory();
                        break;

                    case "9":
                        ShowTotalStockValue();
                        break;

                    case "10":
                        ShowStockValuePerCategory();
                        break;

                    case "11":
                        ShowTop5MostExpensiveProducts();
                        break;

                    case "12":
                        ShowLowStockProducts();
                        break;

                    case "13":
                        ShowOutOfStockProducts();
                        break;

                    case "14":
                        ShowProductSummary();
                        break;

                    case "15":
                        ShowSupplierReport();
                        break;

                    case "16":
                        ShowRecentlyAddedProducts();
                        break;

                    case "17":
                        ShowCategoryStatistics();
                        break;

                    case "18":
                        ShowProductsAboveAveragePrice();
                        break;

                    case "19":
                        SearchAndFilter();
                        break;

                    case "20":
                        ShowProductsPage();
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("====================================");
            Console.WriteLine("       PRODUCT CATALOG SYSTEM");
            Console.WriteLine("====================================");

            Console.WriteLine("1. Get All Available Products");
            Console.WriteLine("2. Filter by Category");
            Console.WriteLine("3. Filter by Price Range");
            Console.WriteLine("4. Search by Product Name");
            Console.WriteLine("5. Sort by Price Ascending");
            Console.WriteLine("6. Sort by Price Descending");
            Console.WriteLine("7. Group Products by Category");
            Console.WriteLine("8. Count Products per Category");
            Console.WriteLine("9. Calculate Total Stock Value");
            Console.WriteLine("10. Stock Value per Category");
            Console.WriteLine("11. Top 5 Most Expensive Products");
            Console.WriteLine("12. Low Stock Products");
            Console.WriteLine("13. Out of Stock Products");
            Console.WriteLine("14. Product Summary");
            Console.WriteLine("15. Supplier Report");
            Console.WriteLine("16. Recently Added Products");
            Console.WriteLine("17. Category Statistics");
            Console.WriteLine("18. Products Above Average Price");
            Console.WriteLine("19. Search + Filter");
            Console.WriteLine("20. Pagination");
            Console.WriteLine("0. Exit");

            Console.WriteLine("====================================");

        }

        private void ShowAvailableProducts()
        {
            var products = _productQueryService.GetAllAvailableProducts();

            ShowProducts(products);
        }
        private void FilterByCategory()
        {
            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            var products = _productQueryService.FilterByCategory(category);

            ShowProducts(products);
        }

        private void FilterByPriceRange()
        {
            Console.Write("Enter minimum price: ");
            decimal min = decimal.Parse(Console.ReadLine());

            Console.Write("Enter maximum price: ");
            decimal max = decimal.Parse(Console.ReadLine());

            var products = _productQueryService.FilterByPriceRange(min, max);

            if (products == null)
            {
                Console.WriteLine("Invalid price range.");
                return;
            }

            ShowProducts(products);
        }

        private void SearchByName()
        {
            Console.Write("Enter product name keyword: ");
            string keyword = Console.ReadLine();

            var products = _productQueryService.SearchByName(keyword);

            if (products == null)
            {
                Console.WriteLine("Keyword cannot be empty.");
                return;
            }

            ShowProducts(products);
        }


        private void GroupProductsByCategory()
        {
            var groups = _productQueryService.GroupByCategory();

            foreach (var group in groups)
            {
                Console.WriteLine();
                Console.WriteLine($"--- {group.Key} ---");

                foreach (var product in group)
                {
                    Console.WriteLine(
                        $"{product.Name} - {product.Price:C}");
                }
            }
        }

        private void CountProductsPerCategory()
        {
            var counts = _productQueryService.CountPerCategory();

            foreach (var item in counts)
            {
                Console.WriteLine($"{item.Key}: {item.Value} products");
            }
        }
        private void ShowTotalStockValue()
        {
            decimal total = _productQueryService.CalculateTotalStockValue();

            Console.WriteLine($"Total Stock Value: {total:C}");
        }

        private void ShowStockValuePerCategory()
        {
            var results = _productQueryService.GetStockValuePerCategory();

            foreach (var item in results)
            {
                Console.WriteLine(
                    $"{item.Category}: {item.StockValue:C}");
            }
        }

        private void ShowTop5MostExpensiveProducts()
        {
            var products =_productQueryService.GetTop5MostExpensiveProducts();

            ShowProducts(products);
        }

        private void ShowLowStockProducts()
        {
            var products = _productQueryService.GetLowStockProducts();

            ShowProducts(products);
        }
        private void ShowOutOfStockProducts()
        {
            var products = _productQueryService.GetOutOfStockProducts();

            ShowProducts(products);
        }
        private void ShowProductSummary()
        {
            var summaries = _productQueryService.GetProductSummary();

            foreach (var product in summaries)
            {
                Console.WriteLine(
                    $"{product.Name} | " +
                    $"Price: {product.Price:C} | " +
                    $"Stock: {product.StockQuantity} | " +
                    $"Status: {product.StockStatus}");
            }
        }

        private void ShowSupplierReport()
        {
            var reports = _productQueryService.GetSupplierReport();

            foreach (var report in reports)
            {
                Console.WriteLine();
                Console.WriteLine($"Supplier: {report.SupplierName}");
                Console.WriteLine($"Products: {report.ProductCount}");
                Console.WriteLine($"Stock Value: {report.StockValue:C}");
                Console.WriteLine($"Average Price: {report.AveragePrice:C}");
            }
        }

        private void ShowRecentlyAddedProducts()
        {
            var products =_productQueryService.GetRecentlyAddedProducts();

            ShowProducts(products);
        }

        private void ShowCategoryStatistics()
        {
            var statistics = _productQueryService.GetCategoryStatistics();

            foreach (var stat in statistics)
            {
                Console.WriteLine();
                Console.WriteLine($"Category: {stat.Category}");
                Console.WriteLine($"Count: {stat.Count}");
                Console.WriteLine($"Average Price: {stat.AveragePrice:C}");
                Console.WriteLine($"Max Price: {stat.MaxPrice:C}");
                Console.WriteLine($"Min Price: {stat.MinPrice:C}");
                Console.WriteLine($"Total Stock Value: {stat.TotalStockValue:C}");
            }
        }
        private void ShowProductsAboveAveragePrice()
        {
            var products =
                _productQueryService.GetProductsAboveAveragePrice();

            ShowProducts(products);
        }

        private void SearchAndFilter()
        {
            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            Console.Write("Enter minimum price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Enter maximum price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Is available? (true/false): ");
            bool isAvailable = bool.Parse(Console.ReadLine());

            var products = _productQueryService.SearchAndFilter(
                category,
                minPrice,
                maxPrice,
                isAvailable);

            ShowProducts(products);
        }

        private void ShowProductsPage()
        {
            Console.Write("Enter page number: ");
            int pageNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter page size: ");
            int pageSize = int.Parse(Console.ReadLine());

            var products =
                _productQueryService.GetProductsPage(
                    pageNumber,
                    pageSize);

            ShowProducts(products);
        }



        private void ShowProducts(List<Product> products)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine(
                    $"ID: {product.ProductId} | " +
                    $"Name: {product.Name} | " +
                    $"Category: {product.Category} | " +
                    $"Price: {product.Price:C} | " +
                    $"Stock: {product.StockQuantity} | " +
                    $"Available: {product.IsAvailable}");
            }
        }
    }

}