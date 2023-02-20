using System.Text.Encodings.Web;
using Identity.Api.Configurations;
using Identity.Api.Constants;
using Identity.Api.Entities;
using Identity.Api.Models.Account;
using Identity.Api.Services.Contracts;
using Identity.Api.Utils;
using Microsoft.AspNetCore.Identity;
using Tcc.Core.Exceptions;

namespace Identity.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly JwtAuthenticationTokenConfiguration _jwtAuthenticationTokenConfiguration;

        public AccountService(
            ILogger<AccountService> logger,
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager, 
            JwtAuthenticationTokenConfiguration jwtAuthenticationTokenConfiguration
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthenticationTokenConfiguration = jwtAuthenticationTokenConfiguration;
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

        public async Task<LoginOutputModel> Login(LoginInputModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new BadRequestException(ErrorMessages.User_NotFound);
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (loginResult.Succeeded)
            {
                var claim = await _userManager.GetClaimsAsync(user);
                return new LoginOutputModel()
                {
                    AccessToken = JwtTokenHelper.GenerateAccessToken(claim, _jwtAuthenticationTokenConfiguration)
                };
            }

            //if (result.RequiresTwoFactor)
            //{

            //}

            if (loginResult.IsLockedOut)
            {
                throw new ForbiddenException(ErrorMessages.AccountLocked);
            }
            else
            {
                throw new ForbiddenException(ErrorMessages.InvalidLoginAttempt);
            }
        }
    }
}
