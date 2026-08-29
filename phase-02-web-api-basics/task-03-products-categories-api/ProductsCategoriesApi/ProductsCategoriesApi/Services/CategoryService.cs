using ProductsCategoriesApi.DTOs;
using ProductsCategoriesApi.Models;

namespace ProductsCategoriesApi.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly List<Category> categories= new List<Category>();

        public CategoryService()
        {
            categories.AddRange(new List<Category>
        {
            new Category
            {
                CategoryId = 1,
                Name = "Electronics",
                Description = "Electronic devices and accessories",
                CreatedAt = DateTime.Now,
                IsActive = true
            },

            new Category
            {
                CategoryId = 2,
                Name = "Furniture",
                Description = "Office and home furniture",
                CreatedAt = DateTime.Now,
                IsActive = true
            },

            new Category
            {
                CategoryId = 3,
                Name = "Stationery",
                Description = "Writing and office supplies",
                CreatedAt = DateTime.Now,
                IsActive = true
            },

            new Category
            {
                CategoryId = 4,
                Name = "Accessories",
                Description = "Bags and laptop accessories",
                CreatedAt = DateTime.Now,
                IsActive = true
            }
        });
        }
        public Category CreateCategory(CreateCategoryRequest request) {
            var uniqueName = categories.Any(s => s.Name.Equals(request.Name,StringComparison.OrdinalIgnoreCase));
            if (uniqueName)
            {
                throw new InvalidOperationException("Category name already exist.");
            }

            var category = new Category
            {
                CategoryId=categories.Count+1,
                Name=request.Name,
                Description=request.Description,
                CreatedAt=DateTime.Now,
                IsActive=true
            };
            categories.Add(category);
            return category;
        }

        public List<Category> GetAll()
        {
            return categories.Where(d => d.IsActive == true).ToList();
        }

        public bool CategoryExists(int categoryId)
        {
            return categories.Any(c =>
                c.CategoryId == categoryId && c.IsActive);
        }

    }
}
