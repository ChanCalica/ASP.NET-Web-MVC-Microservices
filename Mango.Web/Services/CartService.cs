using Mango.Web.Models;
using Mango.Web.Models.Dto.CartDto;
using Mango.Web.Services.IServices;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Services;

public class CartService(IBaseService baseService) : ICartService
{
    public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = cartDto,
            Url = ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
        });
    }

    public async Task<ResponseDto?> EmailCart(CartDto cartDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = cartDto,
            Url = ShoppingCartAPIBase + "/api/cart/EmailCartRequest"
        });
    }

    public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = ShoppingCartAPIBase + $"/api/cart/GetCart/{userId}"
        });
    }

    public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = cartDetailsId,
            Url = ShoppingCartAPIBase + "/api/cart/RemoveCart"
        });
    }

    public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = cartDto,
            Url = ShoppingCartAPIBase + "/api/cart/CartUpsert"
        });
    }
}
