using Mango.Web.Models;
using Mango.Web.Models.Dto.AuthDto;
using Mango.Web.Services.IServices;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Services;

public class AuthService(IBaseService baseService) : IAuthService
{
    public async Task<ResponseDto?> AssignRoleAsync(AssignRoleRequestDto assignRoleRequestDto)
    {
        var response = await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = assignRoleRequestDto,
            Url = AuthAPIBase + "/api/auth/AssignRole"
        });

        return response;
    }

    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = loginRequestDto,
            Url = AuthAPIBase + "/api/auth/login"
        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = registrationRequestDto,
            Url = AuthAPIBase + "/api/auth/register"
        });
    }
}
