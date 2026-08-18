namespace ProductCatalog.Models
{
    public class CategoryStats
    {
        public string Category { get; set; }
        public int Count { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal TotalStockValue { get; set; }
    }
}