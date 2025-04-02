namespace Personal.Shopping.Services.Auth.Application.Dtos;

public class LoginResponseDto
{
    public UserDto? User { get; set; }
    public string? Token { get; set; }
}
