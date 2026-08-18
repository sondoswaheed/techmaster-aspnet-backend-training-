namespace ProductCatalog.Models
{
    public class ProductSummary
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string StockStatus { get; set; }
    }
}