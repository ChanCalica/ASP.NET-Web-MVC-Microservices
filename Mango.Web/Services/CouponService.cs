using Mango.Web.Models;
using Mango.Web.Services.IServices;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Services;

public class CouponService(IBaseService baseService) : ICouponService
{
    public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = couponDto,
            Url = CouponAPIBase + "/api/coupon"
        });
    }

    public async Task<ResponseDto?> DeleteCouponAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = CouponAPIBase + $"/api/coupon/{id}"
        });
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + "/api/coupon"
        });
    }

    public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + $"/api/coupon/GetCouponByCode/{couponCode}"
        });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = CouponAPIBase + $"/api/coupon/{id}"
        });
    }

    public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = couponDto,
            Url = CouponAPIBase + "/api/coupon"
        });
    }
}