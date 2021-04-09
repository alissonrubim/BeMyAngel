﻿using BeMyAngel.Persistance;
using BeMyAngel.Service;
using Microsoft.Extensions.Configuration;

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
                        Host = configuration.GetValue<string>("Service:Persistance:Host"),
                        Username = configuration.GetValue<string>("Service:Persistance:Username"),
                        Password = configuration.GetValue<string>("Service:Persistance:Password"),
                        Database = configuration.GetValue<string>("Service:Persistance:Database"),
                        AutoUpdater = new AutoUpdaterSettings 
                        {
                            Enabled = configuration.GetValue<bool>("Service:Persistance:AutoUpdater:Enabled"),
                            ScriptsDirectory = configuration.GetValue<string>("Service:Persistance:AutoUpdater:ScriptsDirectory")
                        }
                    }
                },
                Security = new SecuritySettings
                {
                    IdentityServer = new IdentityServerSettings
                    {
                        AuthorityUrl = configuration.GetValue<string>("Security:IdentityServer:AuthorityUrl"),
                        ApiName = configuration.GetValue<string>("Security:IdentityServer:ApiName"),
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
