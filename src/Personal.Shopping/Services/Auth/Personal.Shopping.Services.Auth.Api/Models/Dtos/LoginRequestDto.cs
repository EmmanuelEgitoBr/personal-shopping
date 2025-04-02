namespace Personal.Shopping.Services.Auth.Api.Models.Dtos;

public class LoginRequestDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
