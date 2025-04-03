using Personal.Shopping.Services.Auth.Api.Models.Dtos;

namespace Personal.Shopping.Services.Auth.Api.Services.Interfaces;

public interface IAuthService
{
    Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
    Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    Task<ResponseDto> AssignRoleToUser(string email, string roleName);
}
