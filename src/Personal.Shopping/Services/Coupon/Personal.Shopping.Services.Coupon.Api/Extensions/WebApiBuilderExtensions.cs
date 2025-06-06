using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Personal.Shopping.Services.Coupon.Api.Extensions;

public static class WebApiBuilderExtensions
{
    public static void AddSecurityConfiguration(this WebApplicationBuilder builder)
    {
        var secret = builder.Configuration.GetValue<string>("JwtOptions:SecretKey");
        var issuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer");
        var audience = builder.Configuration.GetValue<string>("JwtOptions:Audience");

        var key = Encoding.ASCII.GetBytes(secret!);

        builder.Services.AddAuthentication(c =>
        {
            c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(t =>
        {
            t.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience
            };
        });
        builder.Services.AddAuthorization();
    }

    public static void AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Digite a seguinte expressão para acessar a api: 'Bearer Seu-Token-Jwt'",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    }, new string[]{}
                }
            });
        });
    }
}
