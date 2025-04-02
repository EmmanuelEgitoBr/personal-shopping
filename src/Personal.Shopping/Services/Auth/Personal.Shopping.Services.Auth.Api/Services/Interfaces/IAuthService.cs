using Personal.Shopping.Services.Auth.Api.Models.Dtos;

namespace Personal.Shopping.Services.Auth.Api.Services.Interfaces;

public interface IAuthService
{
    Task<UserDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}
