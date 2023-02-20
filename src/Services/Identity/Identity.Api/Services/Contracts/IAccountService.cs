using Identity.Api.Entities;
using Identity.Api.Models.Account;

namespace Identity.Api.Services.Contracts
{
    public interface IAccountService
    {
        Task<UserEntity> RegisterAsync(RegisterAccountInputModel model);
        Task<LoginOutputModel> Login(LoginInputModel model);
    }
}
