using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; } 
        public Category? Category { get; set; }
        [Required]
        [Range(0.01,int.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public string SupplierName  { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
