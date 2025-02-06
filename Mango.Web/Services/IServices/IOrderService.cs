using Mango.Web.Models;
using Mango.Web.Models.Dto.CartDto;

namespace Mango.Web.Services.IServices;

public interface IOrderService
{
    Task<ResponseDto?> CreateOrderAsync(CartDto cartDto);
}