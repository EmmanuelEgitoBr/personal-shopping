using Personal.Shopping.Manager.Web.Configurations.Resources;
using Personal.Shopping.Manager.Web.Services.Interfaces;

namespace Personal.Shopping.Manager.Web.Services;

public class TokenProviderService : ITokenProviderService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenProviderService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void ClearToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(AuthConstants.TokenCookieKey);
    }

    public string? GetToken()
    {
        string? token = null;
        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies
            .TryGetValue(AuthConstants.TokenCookieKey, out token);

        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(AuthConstants.TokenCookieKey, token);
    }
}
