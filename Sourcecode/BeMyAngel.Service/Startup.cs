using AutoMapper;
using BeMyAngel.Service.Services;
using BeMyAngel.Service.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

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
            _persistanceStartup.ConfigureServices(services);

            /** Register Services **/
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IChatRoomEventService, ChatRoomEventService>();
        }
    }
}
