using RefactoredApi.DTOs;

namespace RefactoredApi.Services
{
    public interface IProductService
    {
        ProductResponse Add(CreateProductRequest request);
        ProductResponse GetById(int id);
        List<ProductResponse> GetAll();
    }
}
