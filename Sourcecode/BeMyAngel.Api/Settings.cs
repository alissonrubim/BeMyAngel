using BeMyAngel.Persistance;
using BeMyAngel.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api
{
    public class Settings
    {
       public ServiceSettings Service { get; set; }
       public SecuritySettings Security { get; set; }

       public static Settings GetSettings(IConfiguration configuration)
       {
            return new Settings
            {
                Service = new ServiceSettings
                {
                    Persistance = new PersistanceSettings
                    {
                        DatabaseConnectionString = configuration.GetValue<string>("Service:Persistance:DatabaseConnectionString")
                    }
                },
                Security = new SecuritySettings
                {
                    IdentityServer = new IdentityServerSettings
                    {
                        AuthorityUrl = configuration.GetValue<string>("Security:IdentityServer:AuthorityUrl"),
                        ApiName = configuration.GetValue<string>("Security:IdentityServer:ApiName"),
                        ApiSecret = configuration.GetValue<string>("Security:IdentityServer:ApiSecret")
                    }
                }
            };
       }
    }

    public class SecuritySettings
    {
        public IdentityServerSettings IdentityServer { get; set; }
    }

    public class IdentityServerSettings
    {
        public string AuthorityUrl { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
    }
}
