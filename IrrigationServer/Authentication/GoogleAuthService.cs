using Google.Apis.Auth;
using IrrigationServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace IrrigationServer.Authentication
{
    public class GoogleAuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public GoogleAuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Authenticate(UserTokenId userToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(userToken.TokenId, new GoogleJsonWebSignature.ValidationSettings());
            return await CreateOrGetUser(payload, userToken);
        }

        private async Task<User> CreateOrGetUser(Payload payload, UserTokenId userToken)
        {
            var user = await _userManager.FindByEmailAsync(payload.Email);

            if(user == null)
            {
                var newUser = new User
                {
                    Email = userToken.Email,
                    UserName = userToken.Email
                };
                var identityUser = await _userManager.CreateAsync(newUser);
                return newUser;
            }

            return user;
        }
    }
}
