using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.IdentityServer.Config
{
    internal class ApiScopes
    {
        public static string Read { get; } = "api.read";
        public static string Write { get; } = "api.write";

        public static IEnumerable<ApiScope> GetScopes()
        {
            return new[]
            {
                new ApiScope(ApiScopes.Read, "Read Access to API"),
                new ApiScope(ApiScopes.Write, "Write Access to API")
            };
        }
    }
}
