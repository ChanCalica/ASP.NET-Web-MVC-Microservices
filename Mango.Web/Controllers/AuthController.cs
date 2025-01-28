using Mango.Web.Models;
using Mango.Web.Models.Dto.AuthDto;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers;

public class AuthController(IAuthService authService,
    ITokenProvider tokenProvider) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();

        return View(loginRequestDto);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto obj)
    {

        ResponseDto? responseDto = await authService.LoginAsync(obj);

        if (responseDto != null && responseDto.IsSuccess)
        {
            LoginResponseDto loginRequestDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Results));

            await SignInUser(loginRequestDto);
            tokenProvider.SetToken(loginRequestDto.Token);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["error"] = responseDto.Message;

            return View(obj);
        }
        //else
        //{
        //    ModelState.AddModelError("CustomError", responseDto.Message);
        //    return View(obj);
        //}
    }

    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = StaticDetail.RoleAdmin,
                Value = StaticDetail.RoleAdmin
            },
            new SelectListItem
            {
                Text = StaticDetail.RoleCustomer,
                Value = StaticDetail.RoleCustomer
            }
        };

        ViewBag.RoleList = roleList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto obj)
    {

        var result = await authService.RegisterAsync(obj);

        ResponseDto assignRole;

        if (result != null && result.IsSuccess)
        {
            if (string.IsNullOrEmpty(obj.Role))
            {
                obj.Role = StaticDetail.RoleCustomer;
            }

            assignRole = await authService.AssignRoleAsync(new AssignRoleRequestDto { Email = obj.Email, Role = obj.Role });

            if (assignRole != null && assignRole.IsSuccess)
            {
                TempData["success"] = "Registration Successful";

                return RedirectToAction(nameof(Login));
            }
        }
        else
        {
            TempData["error"] = result.Message;
        }

        var roleList = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = StaticDetail.RoleAdmin,
                Value = StaticDetail.RoleAdmin
            },
            new SelectListItem
            {
                Text = StaticDetail.RoleCustomer,
                Value = StaticDetail.RoleCustomer
            }
        };

        ViewBag.RoleList = roleList;
        return View(obj);
    }


    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        tokenProvider.ClearToken();

        return RedirectToAction("Index", "Home");
    }

    private async Task SignInUser(LoginResponseDto model)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(model.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Name).Value));

        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(claim => claim.Type == "role").Value));

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}
