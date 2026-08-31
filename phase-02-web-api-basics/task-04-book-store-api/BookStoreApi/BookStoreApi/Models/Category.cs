using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}