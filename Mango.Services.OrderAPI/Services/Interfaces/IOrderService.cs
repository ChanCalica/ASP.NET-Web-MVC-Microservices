using Mango.Services.OrderAPI.Models.Dto;

namespace Mango.Services.OrderAPI.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseDto> CreateOrderAsync(CartDto cartDto);
}
