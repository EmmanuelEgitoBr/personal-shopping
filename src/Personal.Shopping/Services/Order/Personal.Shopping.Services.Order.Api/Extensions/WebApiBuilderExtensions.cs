using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Order.Application.Interfaces;
using Personal.Shopping.Services.Order.Application.Mappings;
using Personal.Shopping.Services.Order.Application.Services;
using Personal.Shopping.Services.Order.Domain.Interfaces;
using Personal.Shopping.Services.Order.Infra.Context;
using Personal.Shopping.Services.Order.Infra.Repositories;

namespace Personal.Shopping.Services.Order.Api.Extensions;

public static class WebApiBuilderExtensions
{
    public static void AddSqlConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            options.EnableSensitiveDataLogging();
        });
    }

    public static void AddApplicationConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
        builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
    }

    public static void AddMapperConfiguration(this WebApplicationBuilder builder)
    {
        IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
        builder.Services.AddScoped<IMapper>(_ => mapper);
        builder.Services.AddAutoMapper(typeof(MappingConfig));
    }
}
