using Amazon.SQS;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Email.Application.Interfaces;
using Personal.Shopping.Services.Email.Application.Mappings;
using Personal.Shopping.Services.Email.Application.Services.Messaging;
using Personal.Shopping.Services.Email.Infra.Context;

namespace Personal.Shopping.Services.Email.Api.Extensions
{
    public static class WebApiBuilderExtensions
    {
        public static void AddSqlConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            });
        }

        public static void AddMapperConfiguration(this WebApplicationBuilder builder)
        {
            IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddAutoMapper(typeof(MappingConfig));
        }

        public static void AddApplicationConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();
            //builder.Services.AddScoped<IProductService, ProductService>();
            //builder.Services.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void AddAwsConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions("AWS"));
            builder.Services.AddAWSService<IAmazonSQS>();
        }
    }
}
