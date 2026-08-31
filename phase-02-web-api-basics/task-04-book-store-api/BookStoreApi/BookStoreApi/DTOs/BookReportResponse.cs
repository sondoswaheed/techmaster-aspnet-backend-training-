using BookStoreAPI.Models;

namespace BookStoreApi.DTOs
{
    public class BookReportResponse
    {
        public int totalBooks { get; set; }
        public decimal totalInventory { get; set; }
        public int availableBooks { get; set; }
        public int OutOfStock {  get; set; }
        public Dictionary<int, int> booksPerCategory { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> booksPerAuthor { get; set; }=new Dictionary<int, int>();
    }
}
