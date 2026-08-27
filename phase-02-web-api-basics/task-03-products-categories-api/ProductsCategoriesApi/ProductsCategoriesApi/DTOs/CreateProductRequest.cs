using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.DTOs
{
    public class CreateProductRequest
    {
        
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0.01, int.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        public string SupplierName { get; set; }
    }
}
