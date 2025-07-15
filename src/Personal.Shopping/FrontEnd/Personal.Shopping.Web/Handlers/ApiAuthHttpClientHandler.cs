using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Personal.Shopping.Web.Handlers;

public class ApiAuthHttpClientHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ApiAuthHttpClientHandler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                              CancellationToken cancellationToken)
    {
        //var token = await _contextAccessor.HttpContext!.GetTokenAsync("access_token");
        var token = _contextAccessor.HttpContext?.Request?.Headers["Authorization"].FirstOrDefault();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
