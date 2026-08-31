using BookStoreAPI.Models;

namespace BookStoreApi.Data
{
    public static class SeedData
    {
        public static List<Author> Authors { get; } = new List<Author>
        {
            new Author
            {
                AuthorId = 1,
                FullName = "Robert C. Martin",
                Country = "USA",
                BirthDate = new DateTime(1952, 12, 5),
                CreatedAt = DateTime.Now
            },

            new Author
            {
                AuthorId = 2,
                FullName = "Martin Fowler",
                Country = "United Kingdom",
                BirthDate = new DateTime(1963, 12, 18),
                CreatedAt = DateTime.Now
            },

            new Author
            {
                AuthorId = 3,
                FullName = "Andrew Hunt",
                Country = "USA",
                BirthDate = new DateTime(1964, 11, 16),
                CreatedAt = DateTime.Now
            },

            new Author
            {
                AuthorId = 4,
                FullName = "Eric Freeman",
                Country = "USA",
                BirthDate = null,
                CreatedAt = DateTime.Now
            }
        };

        public static List<Category> Categories { get; } = new List<Category>
        {
            new Category
            {
                CategoryId = 1,
                Name = "Programming",
                Description = "Books about programming and software development.",
                IsActive = true
            },

            new Category
            {
                CategoryId = 2,
                Name = "Web Development",
                Description = "Books about web development and web technologies.",
                IsActive = true
            },

            new Category
            {
                CategoryId = 3,
                Name = "Software Engineering",
                Description = "Books about software engineering principles and practices.",
                IsActive = true
            },

            new Category
            {
                CategoryId = 4,
                Name = "Design Patterns",
                Description = "Books about software design patterns.",
                IsActive = false
            }
        };

        public static List<Book> Books { get; } = new List<Book>
        {
            new Book
            {
                BookId = 1,
                Title = "Clean Code",
                ISBN = "9780132350884",
                PublishedYear = 2008,
                Price = 650,
                StockQuantity = 10,
                AuthorId = 1,
                CategoryId = 1,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            },

            new Book
            {
                BookId = 2,
                Title = "The Clean Coder",
                ISBN = "9780137081073",
                PublishedYear = 2011,
                Price = 550,
                StockQuantity = 5,
                AuthorId = 1,
                CategoryId = 3,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            },

            new Book
            {
                BookId = 3,
                Title = "Refactoring",
                ISBN = "9780134757599",
                PublishedYear = 2018,
                Price = 700,
                StockQuantity = 7,
                AuthorId = 2,
                CategoryId = 3,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            },

            new Book
            {
                BookId = 4,
                Title = "The Pragmatic Programmer",
                ISBN = "9780135957059",
                PublishedYear = 2019,
                Price = 600,
                StockQuantity = 0,
                AuthorId = 3,
                CategoryId = 1,
                IsAvailable = false,
                CreatedAt = DateTime.Now
            },

            new Book
            {
                BookId = 5,
                Title = "Head First Design Patterns",
                ISBN = "9780596007126",
                PublishedYear = 2004,
                Price = 500,
                StockQuantity = 8,
                AuthorId = 4,
                CategoryId = 4,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            },

            new Book
            {
                BookId = 6,
                Title = "Web Development Fundamentals",
                ISBN = "9781234567890",
                PublishedYear = 2023,
                Price = 450,
                StockQuantity = 12,
                AuthorId = 3,
                CategoryId = 2,
                IsAvailable = true,
                CreatedAt = DateTime.Now
            }
        };
    }
}