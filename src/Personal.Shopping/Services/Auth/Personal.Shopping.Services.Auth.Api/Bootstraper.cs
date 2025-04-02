using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Auth.Api.Models;
using Personal.Shopping.Services.Auth.Infra.Context;
using Personal.Shopping.Services.Auth.Infra.Models;

namespace Personal.Shopping.Services.Auth.Api
{
    public static class Bootstraper
    {
        public static void AddSqlConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            });
        }

        public static void AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        public static void AddAuthConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
        }
    }
}
