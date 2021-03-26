using BeMyAngel.Service.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer.Services
{
    public interface IResourceOwnerValidatorService : IResourceOwnerPasswordValidator
    {
        IEnumerable<Claim> GetUserClaims(User user);
    }

}
