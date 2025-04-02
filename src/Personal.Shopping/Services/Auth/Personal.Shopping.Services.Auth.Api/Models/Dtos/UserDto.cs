namespace Personal.Shopping.Services.Auth.Api.Models.Dtos;

public class UserDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
