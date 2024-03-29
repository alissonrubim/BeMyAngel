﻿using IdentityModel;
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
                    Name = "BeMyAngelApi",
                    DisplayName = "BeMyAngelApi",
                    Description = "Allow the application to access BeMyAngelApi",
                    ApiSecrets = new Secret[] { new Secret("1234".Sha256()) },
                    Scopes = new List<string> {
                        ApiScopes.Write,
                        ApiScopes.Read
                    },
                    UserClaims = new List<string> {
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Role
                    }
                }
            };
        }
    }
}
