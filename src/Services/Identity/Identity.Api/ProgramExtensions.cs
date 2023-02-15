using Identity.Api.Services;
using Identity.Api.Services.Contracts;

namespace Identity.Api
{
    public static class ProgramExtensions
    {
        public static void AddConfigServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IAccountService, AccountService>();
        }
    }
}
