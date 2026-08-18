using ProductCatalog.Data;
using ProductCatalog.Services;
using ProductCatalog.UI;

namespace ProductCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var productService =
                new ProductQueryService(ProductData.Products);

            var menu =
                new ConsoleMenu(productService);

            menu.ShowMenu();
        }
    }
}