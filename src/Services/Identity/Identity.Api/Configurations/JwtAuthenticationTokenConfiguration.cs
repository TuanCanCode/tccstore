using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Configurations
{
    public class JwtAuthenticationTokenConfiguration
    {
        public SymmetricSecurityKey? SecurityKey { get; set; }

        public string? Issuer { get; set; }

        public string? Audience { get; set; }

        public SigningCredentials? SigningCredentials { get; set; }

        public int Expiration { get; set; }
    }
}
