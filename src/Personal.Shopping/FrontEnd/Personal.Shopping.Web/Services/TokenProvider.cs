using Newtonsoft.Json.Linq;
using Personal.Shopping.Web.Configurations.Resources;
using Personal.Shopping.Web.Services.Interfaces;

namespace Personal.Shopping.Web.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenProvider(IHttpContextAccessor contextAccessor)
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
