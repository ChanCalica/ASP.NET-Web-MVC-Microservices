using Mango.Web.Models;
using Mango.Web.Models.Dto.CartDto;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers;

public class CartController(ICartService cartService) : Controller
{
    [Authorize]
    public async Task<IActionResult> CartIndex()
    {


        return View(await LoadCartDtoBasedOnLoggedInUser());
    }

    private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
    {
        var userId = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

        ResponseDto? response = await cartService.GetCartByUserIdAsync(userId);
        if (response != null && response.IsSuccess)
        {
            CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(response.Results.ToString());

            return cartDto;
        }

        return new CartDto();
    }
}
