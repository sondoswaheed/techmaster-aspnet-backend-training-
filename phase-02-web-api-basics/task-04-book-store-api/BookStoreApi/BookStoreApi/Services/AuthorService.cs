using BookStoreApi.Data;
using BookStoreApi.DTOs;
using BookStoreApi.Interfaces;
using BookStoreAPI.Models;

namespace BookStoreApi.Services
{
    public class AuthorService :IAuthorService
    {
        private readonly List<Author> authors = SeedData.Authors;
        
        public Author Create(CreateAuthorRequest request)
        {
            var author = new Author
            {
                Country = request.Country,
                BirthDate = request.BirthDate,
                FullName = request.FullName,
                CreatedAt = DateTime.Now,
                AuthorId = authors.Count + 1
            };
            authors.Add(author);
            return author;
        }

        public List<Author> GetAuthors()
        {
            return authors.ToList();
        }

        public Author? GetById(int id)
        {
            var author=authors.FirstOrDefault(d=>d.AuthorId==id);

            if(author==null) return null;

            return author;
        }
    }
}
