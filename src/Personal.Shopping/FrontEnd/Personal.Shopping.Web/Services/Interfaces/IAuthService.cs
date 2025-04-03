using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Auth;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface IAuthService
{
    Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    Task<ResponseDto> RegisterAsync(RegistrationRequestDto registerRequestDto);
    Task<ResponseDto> AssignRole(AssignRoleRequestDto assignRoleRequestDto);
}
