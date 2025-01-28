using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Services.Interfaces;

public interface IProductService
{
    Task<ResponseDto> GetAllProductsAsync();
    Task<ResponseDto> GetProductByIdAsync(int productId);
    Task<ResponseDto> CreateProductAsync(ProductDto productDto);
    Task<ResponseDto> UpdateProductAsync(ProductDto productDto);
    Task<ResponseDto> DeleteProductAsync(int productId);
}
