using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Reward.Application.Mappings;
using Personal.Shopping.Services.Reward.Infra.Context;
using Personal.Shopping.Services.Reward.Infra.Repositories;

namespace Personal.Shopping.Services.Reward.Api.Extensions;

public static class WebApiBuilderExtensions
{
    public static void AddSqlConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
        });
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
        builder.Services.AddSingleton(new RewardRepository(optionsBuilder.Options));
    }

    public static void AddMapperConfiguration(this WebApplicationBuilder builder)
    {
        IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
        builder.Services.AddSingleton(mapper);
        builder.Services.AddAutoMapper(typeof(MappingConfig));
    }
}
