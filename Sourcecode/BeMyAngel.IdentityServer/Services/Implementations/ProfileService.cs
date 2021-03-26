using BeMyAngel.Service.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;
        private readonly IResourceOwnerValidatorService _resourceOwnerValidatorService;

        public ProfileService(IUserService userService, IResourceOwnerValidatorService resourceOwnerValidatorService)
        {
            _userService = userService;
            _resourceOwnerValidatorService = resourceOwnerValidatorService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Id);
                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    var user = _userService.GetById(int.Parse(userId.Value));
                    if (user != null)
                        context.IssuedClaims = _resourceOwnerValidatorService.GetUserClaims(user).Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                context.IsActive = false;
                var userIdClaim = context.Subject.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Id);
                if (!string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    var user = _userService.GetById(int.Parse(userIdClaim.Value));
                    context.IsActive = user != null && user.IsEnabled;
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
    }
}
