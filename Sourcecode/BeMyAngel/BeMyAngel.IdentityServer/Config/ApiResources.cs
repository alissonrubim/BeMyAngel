using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer.Config
{
    internal static class ApiResources
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "BeMyAngel.Api",
                    DisplayName = "BeMyAngel API",
                    Description = "Allow the application to access the BeMyAngel API on your behalf",
                    Scopes = new List<string> {
                        ApiScopes.Read,
                        ApiScopes.Write
                    },
                    ApiSecrets = new List<Secret> {
                        new Secret("1234")
                    }
                }
            };
        }
    }
}
