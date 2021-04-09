using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Helpers.DatabaseUpdater;
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
            services.AddScoped<IDatabaseUpdater, DatabaseUpdater>();

            /** Register Repositories **/
            services.AddScoped<IDatabaseSchemaRepostory, DatabaseSchemaRepostory>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IChatRoomSessionRepository, ChatRoomSessionRepository>();
            services.AddScoped<IChatRoomEventRepository, ChatRoomEventRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
        }

        public void Configure(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var databaseUpdater = scope.ServiceProvider.GetService<IDatabaseUpdater>();
                databaseUpdater.Update();
            }
        }
    }
}
