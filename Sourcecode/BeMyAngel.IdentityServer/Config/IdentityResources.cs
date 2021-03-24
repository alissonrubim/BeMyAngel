using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer
{
    internal class IdentityResources
    {
        public static string Patient { get; } = "patient";
        public static string Psychiatrist { get; } = "psychiatrist";
        public static string Manager { get; } = "manager";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Email(),
                new IdentityResource
                {
                    Name = JwtClaimTypes.Role,
                    UserClaims = new List<string> {
                        Patient,
                        Psychiatrist,
                        Manager
                    }
                }
            };
        }

    }
}
