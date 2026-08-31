using BookStoreApi.DTOs;
using BookStoreAPI.Models;

namespace BookStoreApi.Interfaces
{
    public interface IAuthorService
    {
        Author Create(CreateAuthorRequest request);
        List<Author> GetAuthors();

        Author? GetById(int id);
    }
}
