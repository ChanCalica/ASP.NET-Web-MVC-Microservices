using Mango.Web.Models;
using Mango.Web.Models.Dto.CartDto;

namespace Mango.Web.Services.IServices;

public interface ICartService
{
    Task<ResponseDto?> GetCartByUserIdAsync(string userId);
    Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
    Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);
    Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
}
