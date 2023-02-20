using System.Text;
using Identity.Api.Configurations;
using Identity.Api.Services;
using Identity.Api.Services.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api
{
    public static class ProgramExtensions
    {
        public static void AddConfigServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IAccountService, AccountService>();
        }

        public static void AddConfigurationServices(this WebApplicationBuilder builder)
        {
            var tokenAuthConfig = new JwtAuthenticationTokenConfiguration();
            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = builder.Configuration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = builder.Configuration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = Convert.ToInt32(builder.Configuration["Authentication:JwtBearer:Expiration"]);
            builder.Services.AddSingleton<JwtAuthenticationTokenConfiguration>(tokenAuthConfig);
        }
    }
}
