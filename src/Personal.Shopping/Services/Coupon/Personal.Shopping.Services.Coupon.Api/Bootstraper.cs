using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Coupon.Infra.Context;

namespace Personal.Shopping.Services.Coupon.Api;

public static class Bootstraper
{
    public static void AddSqlConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
        });
    }
}
