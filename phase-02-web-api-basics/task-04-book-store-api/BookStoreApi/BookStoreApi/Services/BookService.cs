using BookStoreApi.DTOs;
using BookStoreApi.Interfaces;
using BookStoreAPI.Models;
using BookStoreApi.Data;

namespace BookStoreApi.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> books = SeedData.Books;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _auhorService;

        public BookService(ICategoryService categoryService, IAuthorService auhorService)
        {
            _auhorService= auhorService;
            _categoryService= categoryService;
        }
        public BookResponse Create(CreateBookRequest request)
        {
            var category=_categoryService.GetById(request.CategoryId);

            if (category == null)
            {
                throw new InvalidOperationException("Category does not exist.");
            }

            if (!category.IsActive)
            {
                throw new InvalidOperationException(
                    "Inactive category cannot be used for a new book.");
            }

            var author =_auhorService.GetById(request.AuthorId);

            if (author == null)
            {
                throw new InvalidOperationException("Author not exist");
            }


            var ISBNExist = books.Any(k => k.ISBN.Equals(request.ISBN, StringComparison.OrdinalIgnoreCase));

            if (ISBNExist)
            {
                throw new InvalidOperationException("the ISBN already exist");
            }

            var book = new Book
            {
                BookId=books.Count+1,
                AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                Title= request.Title,
                ISBN = request.ISBN,
                PublishedYear= request.PublishedYear,
                IsAvailable=true,
                CreatedAt=DateTime.Now,
                Price=request.Price,
                StockQuantity=request.StockQuantity

            };
            books.Add(book);
            return MapToResponse(book);

        }

        public BookResponse Update(int id,UpdateBookRequest request)
        {
            var book = books.FirstOrDefault(d=>d.BookId== id);

            if (book == null)
            {
                return null;
            }

            var category = _categoryService.GetById(request.CategoryId);

            if (category == null)
            {
                throw new ArgumentException("Category does not exist.");
            }

            if (!category.IsActive)
            {
                throw new InvalidOperationException(
                    "Inactive category cannot be used for a new book.");
            }

            var author = _auhorService.GetById(request.AuthorId);

            if (author == null)
            {
                throw new InvalidOperationException("Author not exist");
            }

            var ISBNExist = books.Any(k => k.ISBN.Equals(request.ISBN, StringComparison.OrdinalIgnoreCase) && k.BookId != id);

            if (ISBNExist)
            {
                throw new InvalidOperationException("the ISBN already exist");
            }

            book.AuthorId = request.AuthorId;
            book.CategoryId = request.CategoryId;
            book.Title = request.Title;
            book.ISBN = request.ISBN;
            book.PublishedYear = request.PublishedYear;
            book.IsAvailable = request.IsAvailable;
            book.Price = request.Price;
            book.StockQuantity = request.StockQuantity;

            return MapToResponse(book);
        }

        public BookResponse GetById(int id)
        {
            var book =books.FirstOrDefault(d=>d.BookId==id);

            if (book == null)
                return null;

            return MapToResponse(book);
        }

        public bool Delete(int id)
        {
            var book = books.FirstOrDefault(d => d.BookId == id);

            if (book == null)
                return false;

            // unavailable
            book.IsAvailable = false;
            book.StockQuantity=0;

            return true;
        }

        public BookReportResponse GetReports()
        {
            var totalBooks = books.Count();

            var availableBooks = books.Where(h => h.IsAvailable).Count();

            var OutOfStock = books.Where(k => k.StockQuantity == 0).Count();

            var booksPerCategory = books.GroupBy(d => d.CategoryId).ToDictionary(
                d => d.Key,
                d => d.Count());

            var booksPerAuthor=books.GroupBy(j=>j.AuthorId).ToDictionary(
                j=>j.Key,
                j => j.Count());

            var totalInventory = books.Sum(j => j.Price * j.StockQuantity);

            return new BookReportResponse
            {
                totalBooks = totalBooks,
                totalInventory = totalInventory,
                booksPerAuthor= booksPerAuthor,
                availableBooks=availableBooks,
                OutOfStock=OutOfStock,
                booksPerCategory=booksPerCategory
            };

        }


        public List<BookResponse> GetAll(BookSearchRequest request)
        {
            var query = books.AsQueryable();

            // Search by Title or ISBN
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(b =>
                    b.Title.Contains(request.Search, StringComparison.OrdinalIgnoreCase) ||
                    b.ISBN.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by Category
            if (request.CategoryId.HasValue)
            {
                query = query.Where(b =>
                    b.CategoryId == request.CategoryId.Value);
            }

            // Filter by Author
            if (request.AuthorId.HasValue)
            {
                query = query.Where(b =>
                    b.AuthorId == request.AuthorId.Value);
            }

            // Filter by Availability
            if (request.IsAvailable.HasValue)
            {
                query = query.Where(b =>
                    b.IsAvailable == request.IsAvailable.Value);
            }

            // Pagination validation
            if (request.PageNumber <= 0)
            {
                throw new InvalidOperationException(
                    "Page number must be greater than 0.");
            }

            if (request.PageSize <= 0)
            {
                throw new InvalidOperationException(
                    "Page size must be greater than 0.");
            }

            var result = query.Skip((request.PageNumber - 1) * request.PageSize).
                Take(request.PageSize).
                Select(MapToResponse).ToList();

            return result;
        }


        public BookResponse MapToResponse(Book book)
        {
            var book1= new BookResponse
            {
                CategoryId=book.CategoryId,
                Title=book.Title,
                PublishedYear=book.PublishedYear,
                AuthorId=book.AuthorId,
                Price=book.Price,
                StockQuantity=book.StockQuantity,
                IsAvailable=book.IsAvailable,
                ISBN=book.ISBN,
                BookId=book.BookId,
                CreatedAt=book.CreatedAt
            };
            return book1;
        }
    }
}
