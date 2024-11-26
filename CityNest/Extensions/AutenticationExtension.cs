using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CityNest
{
    public static class AuthenticationExtension
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["testy-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", policy =>
                {
                    policy.RequireRole("User"); 
                    policy.RequireClaim("User", "true"); 
                });

                options.AddPolicy("AgentPolicy", policy =>
                {
                    policy.RequireClaim("Agent", "true"); 

                    policy.RequireRole("Agent");
                    policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == ClaimTypes.Email && c.Value.EndsWith("@lnu.edu.ua")));
                });
            });
        }
    }
}
