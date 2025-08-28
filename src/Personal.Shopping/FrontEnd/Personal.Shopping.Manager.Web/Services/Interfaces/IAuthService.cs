using Personal.Shopping.Manager.Web.Models;
using Personal.Shopping.Manager.Web.Models.Auth;
using Refit;

namespace Personal.Shopping.Manager.Web.Services.Interfaces;

public interface IAuthService
{
    [Post("/api/auth/assign-role/{assignRoleRequestDto.Email}/{assignRoleRequestDto.RoleName}")]
    Task<ResponseDto> AssignRole([Body] AssignRoleRequestDto assignRoleRequestDto);

    [Post(("/api/auth/login"))]
    Task<ResponseDto> LoginAsync([Body] LoginRequestDto loginRequestDto);

    [Post("/api/auth/register")]
    Task<ResponseDto> RegisterAsync([Body] RegistrationRequestDto registerRequestDto);
}
