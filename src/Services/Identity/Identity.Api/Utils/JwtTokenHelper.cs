using Identity.Api.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.Api.Utils
{
    public static class JwtTokenHelper
    {
        public static string GenerateAccessToken(IList<Claim> claims, JwtAuthenticationTokenConfiguration configuration)
        {
            var now = DateTime.UtcNow;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenOptions = new JwtSecurityToken(
                issuer: configuration.Issuer,
                audience: configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(configuration.Expiration),
                signingCredentials: configuration.SigningCredentials
            );
            return tokenHandler.WriteToken(tokenOptions);
        }
    }
}
