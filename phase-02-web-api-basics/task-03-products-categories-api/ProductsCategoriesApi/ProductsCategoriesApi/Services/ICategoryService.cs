using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services
{
    public interface ICategoryService
    {
        Category CreateCategory (CreateCategoryRequest request);
        List<Category> GetAll();
        bool CategoryExists(int categoryId);

    }
}
