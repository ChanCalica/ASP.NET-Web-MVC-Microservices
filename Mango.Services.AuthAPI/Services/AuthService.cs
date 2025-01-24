using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services;

public class AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
    IJwtTokenGenerator jwtTokenGenerator) : IAuthService
{
    public async Task<ResponseDto> AssignRole(string email, string roleName)
    {
        var user = appDbContext.ApplicationUsers.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());

        if (user != null)
        {
            if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                // Create role if it does not exist
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await userManager.AddToRoleAsync(user, roleName);

            return new()
            {
                IsSuccess = true,
            };
        }

        return new()
        {
            IsSuccess = false,
            Message = "Error Encountered"
        };
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = appDbContext.ApplicationUsers.FirstOrDefault(user => user.UserName.ToLower() == loginRequestDto.Username.ToLower());

        bool isValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (user == null || isValid == false)
        {
            return new LoginResponseDto()
            {
                User = null,
                Token = ""
            };
        }

        // if user was found, Generate JWT Token
        var token = jwtTokenGenerator.GenerateToken(user);

        UserDto userDto = new()
        {
            Email = user.UserName,
            Id = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        return new LoginResponseDto()
        {
            User = userDto,
            Token = token
        };
    }

    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        try
        {
            var result = await userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = appDbContext.ApplicationUsers.First(user => user.UserName == registrationRequestDto.Email);

                UserDto userDto = new()
                {
                    Email = userToReturn.UserName,
                    Id = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };

                return "";
            }
            else
            {
                // Join all error descriptions into a single string
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));

                return errorMessages;
                //return result.Errors.FirstOrDefault().Description;
            }
        }
        catch (Exception ex)
        {
        }

        return "Error Encountered";
    }
}