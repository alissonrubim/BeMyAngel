using BeMyAngel.Persistance;
using BeMyAngel.Service;
using IdentityModel;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeMyAngel.Api
{
    public class Startup
    {
        private const string ApiName = "BeMyAngelApi";
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

            /*services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = ApiName;
                    options.ApiSecret = "Test1234";
                    options.Authority = "https://localhost:5001";
                });*/


            // JWT tokens
            services.AddAuthentication("token").AddJwtBearer("token", options =>
             {
                 options.Authority = "https://localhost:5001";
                 options.Audience = ApiName;
                 var secretKey = "Test1234";
                 var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                 //options.TokenValidationParameters.IssuerSigningKey = signingKey;
             });


            services.AddSwaggerGen(options =>
            {
                var identityServerDisco = GetIdentityServerDiscoveryDocument();
                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = ApiName, Version = "v1" 
                });

                /*options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5051/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5051/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api.read", "api.write"}
                            }
                        }
                    }
                });*/

                options.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
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
