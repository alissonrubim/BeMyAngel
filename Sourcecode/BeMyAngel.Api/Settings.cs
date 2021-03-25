using BeMyAngel.Persistance;
using BeMyAngel.Service;
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
