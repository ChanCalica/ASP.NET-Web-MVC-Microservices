using Mango.Web.Models;
using Mango.Web.Models.Dto.AuthDto;

namespace Mango.Web.Services.IServices;

public interface IAuthService
{
    Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
    Task<ResponseDto?> AssignRoleAsync(AssignRoleRequestDto assignRoleRequestDto);
}
