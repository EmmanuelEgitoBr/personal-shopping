using Amazon.SQS;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Email.Application.Interfaces;
using Personal.Shopping.Services.Email.Application.Mappings;
using Personal.Shopping.Services.Email.Application.Services.Messaging;
using Personal.Shopping.Services.Email.Domain.Interfaces;
using Personal.Shopping.Services.Email.Infra.Context;
using Personal.Shopping.Services.Email.Infra.Repositories;

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
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            builder.Services.AddSingleton(new EmailRepository(optionsBuilder.Options));
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
        }

        public static void AddAwsConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions("AWS"));
            builder.Services.AddAWSService<IAmazonSQS>();
        }
    }
}
