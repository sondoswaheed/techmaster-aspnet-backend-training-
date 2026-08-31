using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs
{
    public class BookResponse
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string ISBN { get; set; } = string.Empty;

        public int PublishedYear { get; set; }
        [Range(.01, int.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime CreatedAt {  get; set; }
    }
}

