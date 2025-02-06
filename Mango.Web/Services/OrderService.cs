using Mango.Web.Models;
using Mango.Web.Models.Dto.CartDto;
using Mango.Web.Services.IServices;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Services;

public class OrderService(IBaseService baseService) : IOrderService
{
    public async Task<ResponseDto?> CreateOrderAsync(CartDto cartDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = cartDto,
            Url = OrderAPIBase + "/api/order/CreateOrder"
        });
    }
}