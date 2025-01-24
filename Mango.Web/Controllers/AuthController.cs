using Mango.Web.Models;
using Mango.Web.Models.Dto.AuthDto;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Web.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();

        return View(loginRequestDto);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = StaticDetails.RoleAdmin,
                Value = StaticDetails.RoleAdmin
            },
            new SelectListItem
            {
                Text = StaticDetails.RoleCustomer,
                Value = StaticDetails.RoleCustomer
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
                obj.Role = StaticDetails.RoleCustomer;
            }

            assignRole = await authService.AssignRoleAsync(new AssignRoleRequestDto { Email = obj.Email, Role = obj.Role });

            if (assignRole != null && assignRole.IsSuccess)
            {
                TempData["success"] = "Registration Successful";

                return RedirectToAction(nameof(Login));
            }
        }

        var roleList = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = StaticDetails.RoleAdmin,
                Value = StaticDetails.RoleAdmin
            },
            new SelectListItem
            {
                Text = StaticDetails.RoleCustomer,
                Value = StaticDetails.RoleCustomer
            }
        };

        ViewBag.RoleList = roleList;

        return View(obj);
    }


    public IActionResult Logout()
    {

        return View();
    }
}
