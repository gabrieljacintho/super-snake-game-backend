using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BertassoGamingServices.Core.Helpers
{
    public static class JwtHelpers
    {
        public static string GetJwtKey(IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }

            return jwtKey;
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var jwtKey = GetJwtKey(configuration);

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        }
    }
}
