using Microsoft.AspNetCore.Authentication.Cookies;
using Personal.Shopping.Web.Services.Base;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services;
using Personal.Shopping.Web.Settings;

namespace Personal.Shopping.Web.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        AppSettings.AuthBaseUrl = builder.Configuration.GetValue<string>("ServicesUrls:AuthService")!;
        AppSettings.CouponBaseUrl = builder.Configuration.GetValue<string>("ServicesUrls:CouponApi")!;
        AppSettings.ProductBaseUrl = builder.Configuration.GetValue<string>("ServicesUrls:ProductApi")!;
        AppSettings.ShoppingCartBaseUrl = builder.Configuration.GetValue<string>("ServicesUrls:ShoppingCartApi")!;

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IBaseService, BaseService>();
        builder.Services.AddScoped<ITokenProvider, TokenProvider>();
        builder.Services.AddScoped<ICouponService, CouponService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
    }

    public static void AddHttpConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("ShoppingApi");
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
}
