using BeMyAngel.Persistance;
using BeMyAngel.Service;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BeMyAngel.Api
{
    public class Startup
    {
        private const string ApiName = "BeMyAngel.Api";
        private readonly Service.Startup _serviceStartUp;
        private Settings _settings;
        public Startup(IConfiguration configuration)
        {
            _settings = GetSettings(configuration);
            _serviceStartUp = new Service.Startup(_settings.Service);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = _settings.Security.IdentityServer.Url;
                options.Audience = ApiName;
                options.RequireHttpsMetadata = false;
            });

            services.AddSwaggerGen(options =>
            {
                var identityServerDisco = GetIdentityServerDiscoveryDocument();
                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = ApiName, Version = "v1" 
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(identityServerDisco.AuthorizeEndpoint),
                            TokenUrl = new Uri(identityServerDisco.TokenEndpoint),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api.read", "Allows to read information from API"},
                                {"api.write", "Allows to write information to API"}
                            }
                        }
                    }
                });
            });

            _serviceStartUp.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1.0");
                });
            }
        }

        private Settings GetSettings(IConfiguration configuration)
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
                        Url = configuration.GetValue<string>("Security:IdentityServer:Url")
                    }
                }
            };
        }

        private DiscoveryDocumentResponse GetIdentityServerDiscoveryDocument()
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync(_settings.Security.IdentityServer.Url).Result;
            if (disco.IsError) 
                throw new Exception($"It was not possible to connect to the identity server. Error: {disco.Error}");
            return disco;
        }
    }
}
