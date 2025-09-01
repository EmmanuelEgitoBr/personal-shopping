using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using System.Text;

namespace Personal.Shopping.GatewaySolution.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddOcelotConfig(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("ocelotsettings.json", 
                optional: false, 
                reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);
        }

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
    }
}
