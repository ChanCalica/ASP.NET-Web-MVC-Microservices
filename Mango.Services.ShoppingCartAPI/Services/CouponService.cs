using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Services.Interfaces;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Services;

public class CouponService(IHttpClientFactory httpClientFactory) : ICouponService
{
    public async Task<CouponDto> GetCouponByCode(string couponCode)
    {
        var client = httpClientFactory.CreateClient("Coupon");
        var response = await client.GetAsync($"/api/coupon/GetCouponByCode/{couponCode}");
        var apiContent = await response.Content.ReadAsStringAsync();
        var resposeDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

        if (resposeDto.IsSuccess)
        {
            return JsonConvert.DeserializeObject<CouponDto>(resposeDto.Results.ToString());
        }

        return new CouponDto();
    }
}
