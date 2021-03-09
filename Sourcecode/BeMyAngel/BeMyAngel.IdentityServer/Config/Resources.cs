using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer
{
    internal class Resources
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return null;/*new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "Roles",
                    UserClaims = new List<string> { "patient", "psychiatrist" }
                }
            };*/
        }

    }
}
