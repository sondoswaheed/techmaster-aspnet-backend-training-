using System.ComponentModel.DataAnnotations;

namespace RefactoredApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
