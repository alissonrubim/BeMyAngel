using BeMyAngel.IdentityServer.Config;
using BeMyAngel.IdentityServer.Services;
using BeMyAngel.IdentityServer.Services.Implementations;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace BeMyAngel.IdentityServer
{
    public class Startup
    {
        private readonly Service.Startup _serviceStartUp;
        private Settings _settings;
        public Startup(IConfiguration configuration)
        {
            _settings = Settings.GetSettings(configuration);
            _serviceStartUp = new Service.Startup(_settings.Service);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var clients = Clients.GetClients();

            /** Get all the cors configuration from the Clients and use it to enable Cors on the service **/
            var corsOrigins = new List<string>();
            foreach (var client in clients)
                corsOrigins.AddRange(client.AllowedCorsOrigins);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPermission", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(corsOrigins.ToArray())
                        .AllowCredentials();
                });
            });

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddInMemoryClients(clients)
                .AddInMemoryIdentityResources(IdentityResources.GetIdentityResources())
                .AddInMemoryApiResources(ApiResources.GetResources())
                .AddInMemoryApiScopes(ApiScopes.GetScopes())
                .AddTestUsers(TestUsers.GetTestUsers())
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerValidatorService>();
            services.AddTransient<IResourceOwnerValidatorService, ResourceOwnerValidatorService>();
            services.AddTransient<IProfileService, ProfileService>();


            //LEIA SOBRE COMO COLOCAR O ACESSO AO USUARIO AQUI https://stackoverflow.com/questions/35304038/identityserver4-register-userservice-and-get-users-from-database-in-asp-net-core
            /* Not enabled for now */
            /* services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                });
            */

            services.AddControllersWithViews();

            _serviceStartUp.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPermission");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

            _serviceStartUp.Configure(app.ApplicationServices);
        }
    }
}
