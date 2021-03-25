using AutoMapper;
using BeMyAngel.Service.Services.ChatRoomService;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BeMyAngel.Service
{
    public class Startup
    {
        private readonly Persistance.Startup _persistanceStartup;
        private readonly ServiceSettings _settings;

        public Startup(ServiceSettings settings)
        {
            _settings = settings;
            _persistanceStartup = new Persistance.Startup(_settings.Persistance);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(_settings);
            services.AddScoped<IChatRoomService, ChatRoomService>();
            _persistanceStartup.ConfigureServices(services);
        }
    }
}
