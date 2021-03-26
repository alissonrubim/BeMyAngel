using BeMyAngel.Persistance;
using BeMyAngel.Service;
using Microsoft.Extensions.Configuration;

namespace BeMyAngel.IdentityServer
{
    public class Settings
    {
       public ServiceSettings Service { get; set; }

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
                }
            };
       }
    }
}
