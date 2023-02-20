﻿using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.Api.Services
{
    public class UserManager : UserManager<UserEntity>
    {
        public UserManager(
            IUserStore<UserEntity> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserEntity> passwordHasher,
            IEnumerable<IUserValidator<UserEntity>> userValidators,
            IEnumerable<IPasswordValidator<UserEntity>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<UserEntity>> logger
            ) : base(store, optionsAccessor, passwordHasher, userValidators,
            passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
