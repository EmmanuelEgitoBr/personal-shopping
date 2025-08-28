using Microsoft.AspNetCore.Authentication.Cookies;
using Personal.Shopping.Manager.Web.Services;
using Personal.Shopping.Manager.Web.Services.Interfaces;
using Refit;

namespace Personal.Shopping.Manager.Web.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddRefitServices(this WebApplicationBuilder builder, 
            IConfiguration configuration)
        {
            builder.Services
                .AddRefitClient<IProductService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Product"]!));

            builder.Services
                .AddRefitClient<ICategoryService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Product"]!));

            builder.Services
                .AddRefitClient<IAuthService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:Auth"]!));
        }

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
    }
}
