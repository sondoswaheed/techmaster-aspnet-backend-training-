using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        
        [Required]
        public string FullName { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}