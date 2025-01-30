using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Services.Interfaces;

public interface ICouponService
{
    Task<CouponDto> GetCouponByCode(string couponCode);
}
