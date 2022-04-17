using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PointOfSales.Web.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddTokenAuthentication(
            this IServiceCollection services,
            IConfiguration config)
        {
            var jwtConfig = config.GetSection("JwtConfig");
            string secret = jwtConfig.GetSection("secret").Value;
            string issuer = jwtConfig.GetSection("issuer").Value;
            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x => {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer
                    };

                    x.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            // Call this to skip the default logic and avoid using the default response
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.Headers["content-type"] = "application/json";
                            await context.Response.WriteAsync("{\"success\": false, \"message\":\"Token is not valid\"}");
                        }
                    };
                });

            return services;
        }
    }
}
