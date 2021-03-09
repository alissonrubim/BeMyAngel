using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace BeMyAngel.IdentityServer.Config
{
    internal class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "BeMyAngel.WebApp",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("q1w2e3")
                    },
                    RedirectUris = {
                        "https://localhost:44301/swagger/oauth2-redirect.html"
                    },
                    AllowedScopes = {
                        "api.read",
                        "api.write"
                    }
                }
            };
        }
    }
}
