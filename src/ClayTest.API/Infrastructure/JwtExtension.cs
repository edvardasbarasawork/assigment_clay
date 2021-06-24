using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClayTest.API.Infrastructure
{
    public static class JwtExtension
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("JwtConfiguration:SecretKey");

            var key = Encoding.UTF8.GetBytes(secretKey);
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                };
            });

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var token = new JwtSecurityToken(claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, "UniqueName"),
                new Claim(JwtRegisteredClaimNames.Email, "Email"),
                new Claim(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString())
            },
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
