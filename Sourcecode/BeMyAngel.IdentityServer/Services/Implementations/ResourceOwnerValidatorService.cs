using BeMyAngel.Service.Helpers;
using BeMyAngel.Service.Models;
using BeMyAngel.Service.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer.Services.Implementations
{
    public class ResourceOwnerValidatorService : IResourceOwnerValidatorService
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public ResourceOwnerValidatorService(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (string.IsNullOrEmpty(context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Password must be defined");
                return;
            }

            var user = _userService.GetByUserName(context.UserName);
            if (user != null)
            { 
                if (user.Password == Security.GetEncryptedPassword(context.Password, user.EncryptKey))
                {
                    context.Result = new GrantValidationResult(
                        subject: user.UserName,
                        authenticationMethod: "password",
                        claims: GetUserClaims(user)
                    );
                    return;
                }

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                return;
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
            return;
        }

        public IEnumerable<Claim> GetUserClaims(User user)
        {
            var roles = _userRoleService.GetRolesByUserId(user.UserId);
            var claims =  new List<Claim>()
            {
                new Claim(JwtClaimTypes.Id, user.UserId.ToString()),
                new Claim(JwtClaimTypes.Email, user.UserName) //Username is the email
            };

            foreach (var role in roles)
                claims.Add(new Claim(JwtClaimTypes.Role, role.Identifier));

            return claims;
        }
    }
}
