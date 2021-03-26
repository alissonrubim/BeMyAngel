using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Persistance.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BeMyAngel.Persistance
{
    public class Startup
    {
        private readonly PersistanceSettings _settings;
        public Startup(PersistanceSettings settings)
        {
            _settings = settings;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_settings);
            services.AddScoped<IDatabase, Database>();

            /** Register Repositories **/
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }
    }
}
