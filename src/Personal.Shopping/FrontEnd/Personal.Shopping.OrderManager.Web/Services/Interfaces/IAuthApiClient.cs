using Personal.Shopping.OrderManager.Web.Models;
using Personal.Shopping.OrderManager.Web.Models.Auth;
using Refit;

namespace Personal.Shopping.OrderManager.Web.Services.Interfaces;

public interface IAuthApiClient
{
    [Post("/api/auth/assign-role/{assignRoleRequestDto.Email}/{assignRoleRequestDto.RoleName}")]
    Task<ResponseDto> AssignRole([Body] AssignRoleRequestDto assignRoleRequestDto);

    [Post(("/api/auth/login"))]
    Task<ResponseDto> LoginAsync([Body] LoginRequestDto loginRequestDto);

    [Post("/api/auth/register")]
    Task<ResponseDto> RegisterAsync([Body] RegistrationRequestDto registerRequestDto);
}
