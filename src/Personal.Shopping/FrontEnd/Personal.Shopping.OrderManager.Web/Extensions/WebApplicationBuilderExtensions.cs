using Microsoft.AspNetCore.Authentication.Cookies;
using Personal.Shopping.OrderManager.Web.Services;
using Personal.Shopping.OrderManager.Web.Services.Interfaces;
using Refit;

namespace Personal.Shopping.OrderManager.Web.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ITokenProviderService, TokenProviderService>();
    }

    public static void AddAuthConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });
    }

    public static void AddRefitServices(this WebApplicationBuilder builder,
            IConfiguration configuration)
    {
        builder.Services
            .AddRefitClient<IProductApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Product"]!));

        builder.Services
            .AddRefitClient<ICategoryApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Product"]!));

        builder.Services
            .AddRefitClient<IAuthApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Auth"]!));

        builder.Services
            .AddRefitClient<ICouponApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Coupon"]!));

        builder.Services
            .AddRefitClient<IOrderApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Order"]!));
    }
}
