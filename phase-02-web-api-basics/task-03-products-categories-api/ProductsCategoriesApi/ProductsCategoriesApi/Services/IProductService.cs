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
        List<ProductResponse> SearchByName(string? name ,int?categoryId ,decimal? minPrice ,decimal? maxPrice ,bool? isAvailable, bool? lowStock);
        StockReportResponse StockReport();
        ProductResponse UpdateStock(int id, UpdateStockRequest request);

        List<ProductResponse> LowStock();

    }
}
