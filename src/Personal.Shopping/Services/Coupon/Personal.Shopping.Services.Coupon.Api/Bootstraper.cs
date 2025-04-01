using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Coupon.Application.Interfaces;
using Personal.Shopping.Services.Coupon.Application.Mappings;
using Personal.Shopping.Services.Coupon.Application.Services;
using Personal.Shopping.Services.Coupon.Domain.Interfaces;
using Personal.Shopping.Services.Coupon.Infra.Context;
using Personal.Shopping.Services.Coupon.Infra.Repositories;

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

    public static void AddApplicationConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICouponService, CouponService>();
        builder.Services.AddScoped<ICouponRepository, CouponRepository>();
    }

    public static void AddMapperConfiguration(this WebApplicationBuilder builder)
    {
        IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
        builder.Services.AddSingleton(mapper);
        builder.Services.AddAutoMapper(typeof(MappingConfig));
    }
}
