using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Kolosok.Application.Interfaces.Persistence;
using Microsoft.IdentityModel.Tokens;

namespace Kolosok.Persistence.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey("superSecretKey@345superSecretKey@345superSecretKey@345"u8.ToArray());
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var cleanedClaims = claims.Select(claim =>
            {
                var type = claim.Type
                    .Replace("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/", "")
                    .Replace("http://schemas.microsoft.com/ws/2008/06/identity/claims/", "");
                return new Claim(type, claim.Value);
            });
            
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: cleanedClaims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey("superSecretKey@345superSecretKey@345superSecretKey@345"u8.ToArray()),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
