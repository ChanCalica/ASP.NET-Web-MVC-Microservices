using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Services.Interfaces;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Services;

public class ProductService(IHttpClientFactory httpClientFactory) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var client = httpClientFactory.CreateClient("Product");
        var response = await client.GetAsync("/api/product");
        var apiContent = await response.Content.ReadAsStringAsync();
        var resposeDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

        if (resposeDto.IsSuccess)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(resposeDto.Results.ToString());
        }

        return new List<ProductDto>();
    }

    public async Task<ProductDto> GetProductByIdAsync(int productId)
    {
        var client = httpClientFactory.CreateClient("Product");
        var response = await client.GetAsync($"/api/product/{productId}");
        var apiContent = await response.Content.ReadAsStringAsync();
        var resposeDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

        if (resposeDto.IsSuccess)
        {
            return JsonConvert.DeserializeObject<ProductDto>(resposeDto.Results.ToString());
        }

        return new ProductDto();
    }
}
