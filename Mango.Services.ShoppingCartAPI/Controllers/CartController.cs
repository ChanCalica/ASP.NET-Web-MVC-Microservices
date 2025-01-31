using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController(ICartService cartService) : ControllerBase
{
    [HttpGet("GetCart/{userId}")]
    public async Task<IActionResult> GetCart(string userId)
    {
        var response = await cartService.GetCartAsyncVersionTwo(userId);

        return Ok(response);
    }

    [HttpPost("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon([FromBody] CartDto cartDto)
    {
        var response = await cartService.ApplyCouponAsync(cartDto);

        return Ok(response);
    }

    [HttpPost("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon([FromBody] CartDto cartDto)
    {
        var response = await cartService.RemoveCouponAsync(cartDto);
        return Ok(response);
    }

    [HttpPost("CartUpsert")]
    public async Task<IActionResult> CartUpsert(CartDto cartDto)
    {
        var response = await cartService.CartUpsertAsync(cartDto);

        return Ok(response);
    }

    [HttpPost("RemoveCart")]
    public async Task<IActionResult> RemoveCart([FromBody] int cartDetailsId)
    {
        var response = await cartService.RemoveCartAsync(cartDetailsId);

        return Ok(response);
    }
}
