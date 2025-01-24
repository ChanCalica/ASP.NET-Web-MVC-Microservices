namespace Mango.Web.Models.Dto.AuthDto;

public class LoginResponseDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}