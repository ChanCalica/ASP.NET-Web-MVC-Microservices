namespace Mango.Services.AuthAPI.Models.Dto;

public class AssignRoleRequestDto
{
    public required string Email { get; set; }
    public required string Role { get; set; }
}