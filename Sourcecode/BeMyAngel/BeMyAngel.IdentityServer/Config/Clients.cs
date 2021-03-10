using IdentityServer4;
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
        private static Client CreateApplicationClientWithPKCE(string clientId, string clientName, string clientSecret, params string[] allowedScopes) =>
            new Client
            {
                ClientId = clientId,
                ClientName = clientName,
                RequireConsent = false,
                RequireClientSecret = false,
                RequirePkce = true,
                AllowedGrantTypes = IdentityServer4.Models.GrantTypes.CodeAndClientCredentials,
                AccessTokenLifetime = 60*60,
                AllowAccessTokensViaBrowser = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                AlwaysSendClientClaims = true,
                AllowedScopes = allowedScopes.Concat(new[]
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }).ToArray(),
                //where to redirect to after login
                RedirectUris = new[] { "https://localhost:44301" },
                // where to redirect to after logout
                PostLogoutRedirectUris = new[] { "https://localhost:44301" },
                AllowedCorsOrigins = new[] {
                    "https://localhost:44301"
                },
                ClientSecrets =
                {
                    new Secret(clientSecret)
                }
            };

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                CreateApplicationClientWithPKCE("BeMyAngel.WebApp", "BeMyAngel Web Application", "1234", new[]{ ApiScopes.Read, ApiScopes.Write })
            };
        }
    }
}
