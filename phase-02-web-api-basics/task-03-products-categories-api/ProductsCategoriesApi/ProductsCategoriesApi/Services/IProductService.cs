using ProductsCategoriesApi.DTOs;

namespace ProductsCategoriesApi.Services
{
    public interface IProductService
    {
        ProductResponse Create(CreateProductRequest request);
        ProductResponse Update(int id,UpdateProductRequest request);
        List<ProductResponse> GetAll();
        ProductResponse GetById(int id);
        bool Delete(int id);
    }
}
