using Mango.Web.Models;
using Mango.Web.Models.Dto.Product;

namespace Mango.Web.Services.IServices;

public interface IProductService
{
    Task<ResponseDto?> GetALlProductsAsync();
    Task<ResponseDto?> GetProductByIdAsync(int id);
    Task<ResponseDto?> CreateProductAsync(ProductDto product);
    Task<ResponseDto?> UpdateProductAsync(ProductDto product);
    Task<ResponseDto?> DeleteProductAsync(int id);
}
