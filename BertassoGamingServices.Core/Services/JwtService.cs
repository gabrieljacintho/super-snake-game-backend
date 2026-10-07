using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BertassoGamingServices.Core.Domain.IdentityEntities;
using BertassoGamingServices.Core.DTOs;
using BertassoGamingServices.Core.Helpers;
using BertassoGamingServices.Core.ServiceContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BertassoGamingServices.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse CreateJwtToken(ApplicationUser user, IEnumerable<string> roles)
        {
            DateTime expiration = GetExpirationDateTime("Jwt");

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtHelpers.GetJwtKey(_configuration)));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return new AuthenticationResponse
            {
                Name = user.Name,
                Email = user.Email,
                Roles = roles.ToList(),
                Token = token,
                Expiration = expiration,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = GetExpirationDateTime("RefreshToken")
            };
        }

        private static string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

        private DateTime GetExpirationDateTime(string configurationValue)
        {
            return DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration[$"{configurationValue}:EXPIRATION_MINUTES"]));
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string token)
        {
            var tokenValidationParameters = JwtHelpers.GetTokenValidationParameters(_configuration);
            tokenValidationParameters.ValidateLifetime = false; // Ignore token expiration for refresh token validation

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
