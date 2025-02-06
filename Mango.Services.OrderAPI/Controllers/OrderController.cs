using Mango.Services.OrderAPI.Models.Dto;
using Mango.Services.OrderAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.OrderAPI.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("CreateOrder")]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] CartDto cartDto)
    {
        var response = await _orderService.CreateOrderAsync(cartDto);

        return Ok(response);
    }
}
