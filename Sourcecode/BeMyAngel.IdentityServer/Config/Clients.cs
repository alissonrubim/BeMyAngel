using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeMyAngel.IdentityServer.Config
{

    internal static class ClientsHelper
    {
        public static Client CreateResourceOwnerPasswordClient(string clientId, string clientName, string[] allowedScopes, string[] allowedCorsOrigins) =>
            new Client
            {
                ClientId = clientId,
                ClientName = clientName,
                AccessTokenLifetime = 24*60*60,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = allowedScopes,
                AlwaysIncludeUserClaimsInIdToken = true,
                AlwaysSendClientClaims = true,
                RequireClientSecret = false,
                AllowedCorsOrigins = allowedCorsOrigins
            };
    }

    internal class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>() {
                ClientsHelper.CreateResourceOwnerPasswordClient("AndroidApp.BeMyAngel", "BeMyAngel Android Application", new string[]
                {
                    ApiScopes.Read,
                    ApiScopes.Write
                }, 
                new string[]
                {
                    "http://localhost:3000",
                    "https://localhost:5001"
                })
            };
                
                /*
            new Client
                {
                    ClientId = "BeMyAngelWebApp",
                    ClientName = "Main BeMyAngel WebApplication",
                    ClientSecrets = new List<Secret> {new Secret("Test1234".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequirePkce = false,
                    AllowPlainTextPkce = false,
                    AllowedCorsOrigins = new string[] { 
                        "http://localhost:3000",
                        "https://localhost:5001"
                    },
                    RedirectUris = new List<string> {
                        "http://localhost:3000",
                        "http://localhost:3000/authentication/google",

                        "https://localhost:5000",
                        "https://localhost:5001/swagger/oauth2-redirect.html",
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        JwtClaimTypes.Role,
                        ApiScopes.Read,
                        ApiScopes.Write
                    }
                }
            };*/
        }
    }
}
