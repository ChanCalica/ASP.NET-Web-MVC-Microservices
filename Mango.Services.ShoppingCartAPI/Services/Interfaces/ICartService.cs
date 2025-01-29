using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Services.Interfaces;

public interface ICartService
{
    Task<ResponseDto> GetCartAsync(string userId);
    Task<ResponseDto> ApplyCouponAsync(CartDto cartDto);
    Task<ResponseDto> RemoveCouponAsync(CartDto cartDto);
    Task<ResponseDto> GetCartAsyncVersionTwo(string userId);
    Task<ResponseDto> CartUpsertAsync(CartDto cartDto);
    Task<ResponseDto> RemoveCartAsync(int cartDetailsId);
}
