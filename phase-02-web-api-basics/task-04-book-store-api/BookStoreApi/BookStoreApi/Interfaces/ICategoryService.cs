using BookStoreApi.DTOs;
using BookStoreAPI.Models;

namespace BookStoreApi.Interfaces
{
    public interface ICategoryService
    {
        CategoryResponse Create(CreateCategoryRequest request);
        List<CategoryResponse> GetAll();
        Category? GetById(int id);
    }
}
