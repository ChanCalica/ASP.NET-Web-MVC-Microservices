using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    protected ResponseDto _responseDto = new();

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
    {
        var errorMessage = await authService.Register(registrationRequestDto);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = errorMessage;

            return BadRequest(_responseDto);
        }

        return Ok(_responseDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var loginResponse = await authService.Login(loginRequestDto);

        if (loginResponse.User == null)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Username or password is incorrect";
            return BadRequest(_responseDto);
        }

        _responseDto.Results = loginResponse;

        return Ok(_responseDto);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto assignRoleRequestDto)
    {
        var assignRoleSuccessful = await authService.AssignRole(assignRoleRequestDto.Email, assignRoleRequestDto.Role.ToUpper());

        if (!assignRoleSuccessful.IsSuccess)
        {
            return BadRequest(assignRoleSuccessful);
        }

        return Ok(assignRoleSuccessful);
    }
}