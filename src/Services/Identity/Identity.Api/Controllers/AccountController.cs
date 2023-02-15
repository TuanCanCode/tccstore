using Identity.Api.Constants;
using Identity.Api.Entities;
using Identity.Api.Models.Account;
using Identity.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        /// <summary>
        /// AccountController
        /// </summary>
        public AccountController(
            IAccountService accountService
            )
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<UserEntity> Register([FromBody] RegisterAccountInputModel model)
        {
            if (model == null) throw new BadHttpRequestException(ErrorMessages.InvalidRequest);
            return await _accountService.RegisterAsync(model);
        }
    }
}
