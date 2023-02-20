using System.Text.Encodings.Web;
using Identity.Api.Entities;
using Identity.Api.Models.Account;
using Identity.Api.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Tcc.Core.Exceptions;

namespace Identity.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<UserEntity> _userManager;

        public AccountService(
            UserManager<UserEntity> userManager,
            ILogger<AccountService> logger
        )
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<UserEntity> RegisterAsync(RegisterAccountInputModel model)
        {
            var user = new UserEntity() { Email = model.Email, UserName = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            }
            else
            {
                throw new BadRequestException(result.Errors.First().Description);
            }

            return user;
        }
    }
}
