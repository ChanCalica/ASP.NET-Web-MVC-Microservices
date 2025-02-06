using Mango.Web.Models;
using Mango.Web.Models.Dto.Product;
using Mango.Web.Services.IServices;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Services;

public class ProductService(IBaseService baseService) : IProductService
{
    public async Task<ResponseDto?> CreateProductAsync(ProductDto product)
    {
        var response = await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Data = product,
            Url = ProductAPIBase + "/api/product"
        });

        return response;
    }

    public async Task<ResponseDto?> DeleteProductAsync(int id)
    {
        var response = await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.DELETE,
            Url = ProductAPIBase + $"/api/product/{id}"
        });

        return response;
    }

    public async Task<ResponseDto?> GetALlProductsAsync()
    {
        var response = await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = ProductAPIBase + "/api/product"
        });

        return response;
    }

    public async Task<ResponseDto?> GetProductByIdAsync(int id)
    {
        var response = await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = ProductAPIBase + $"/api/product/{id}"
        });

        return response;
    }

    public async Task<ResponseDto?> UpdateProductAsync(ProductDto product)
    {
        var response = await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.PUT,
            Data = product,
            Url = ProductAPIBase + "/api/product"
        });

        return response;
    }
}
