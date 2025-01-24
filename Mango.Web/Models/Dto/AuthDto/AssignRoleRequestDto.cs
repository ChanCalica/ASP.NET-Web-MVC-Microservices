namespace Mango.Web.Models.Dto.AuthDto;

public class AssignRoleRequestDto
{
    public required string Email { get; set; }
    public required string Role { get; set; }
}