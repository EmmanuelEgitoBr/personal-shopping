using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Auth;
using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;

namespace Personal.Shopping.Web.Services;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;

    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> AssignRole(AssignRoleRequestDto assignRoleRequestDto)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = assignRoleRequestDto,
            Url = AppSettings.AuthBaseUrl + $"/api/auth/assign-role/{assignRoleRequestDto.Email}/{assignRoleRequestDto.RoleName}"
        };
        return await _baseService.SendAsync(request)!;
    }

    public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = loginRequestDto,
            Url = AppSettings.AuthBaseUrl + $"/api/auth/login"
        };
        return await _baseService.SendAsync(request)!;
    }

    public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registerRequestDto)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = registerRequestDto,
            Url = AppSettings.AuthBaseUrl + $"/api/auth/register"
        };
        return await _baseService.SendAsync(request)!;
    }
}
