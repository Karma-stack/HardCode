using HardCode.WebAPI.Entities;
using HardCode.WebAPI.Models;

namespace HardCode.WebAPI.Interfaces
{
    public interface IProductService
    {
        Task<ResponseModel<ProductRequestModel>> CreateProductAsync(ProductRequestModel model);

        Task<ResponseModel<Product>> UpdateProductAsync(int productId, ProductRequestModel model);

        Task<ResponseModel<Product>> DeleteProductAsync(int productId);
        Task<ResponseModel<List<Product>>> GetProductsListAsync(string search);
        Task<ResponseModel<Product>> GetProductAsync(int productId);
    }
}
