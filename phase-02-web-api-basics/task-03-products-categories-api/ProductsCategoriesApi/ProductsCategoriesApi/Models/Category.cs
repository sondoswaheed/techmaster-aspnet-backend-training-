using System.ComponentModel.DataAnnotations;

namespace ProductsCategoriesApi.Models
{
    public class Category
    {
        public int CategoryId {  get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; }=new List<Product>();
    }
}
