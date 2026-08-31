using BookStoreApi.DTOs;
using BookStoreApi.Interfaces;
using BookStoreAPI.Models;
using BookStoreApi.Data;

namespace BookStoreApi.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly List<Category> categories = SeedData.Categories;

        public CategoryResponse Create(CreateCategoryRequest request)
        {
            var categoryExist = categories.Any(s => s.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
            if (categoryExist)
            {
                throw new InvalidOperationException("name already exist");
            }
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = categories.Count + 1,
                IsActive = true
            };
            categories.Add(category);
            return MapToResponse(category);
        }

        public List<CategoryResponse> GetAll()
        {
            return categories.Select(MapToResponse).ToList();
        }

        public CategoryResponse MapToResponse(Category category)
        {
            return new CategoryResponse
            {
                Name = category.Name,
                Description = category.Description,
                CategoryId = category.CategoryId,
                IsActive = category.IsActive
            };
        }

        public Category GetById(int id)
        {
            var category=categories.FirstOrDefault(g=>g.CategoryId == id);

            if (category == null)
                return null;

            return category;
        }
    }
}
