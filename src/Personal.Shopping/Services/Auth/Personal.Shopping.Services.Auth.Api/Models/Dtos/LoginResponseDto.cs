namespace Personal.Shopping.Services.Auth.Api.Models.Dtos;

public class LoginResponseDto
{
    public UserDto? User { get; set; }
    public string? Token { get; set; }
}
