using BeMyAngel.Persistance.Helpers;
using BeMyAngel.Persistance.Repositories.ChatRoom;
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
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        }
    }
}
