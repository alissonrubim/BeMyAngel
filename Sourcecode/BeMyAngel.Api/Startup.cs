using BeMyAngel.Api.Helpers.SessionManager;
using BeMyAngel.Api.Hubs;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Net.Http;

namespace BeMyAngel.Api
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            /** Add Signal IR **/
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPermission", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(new[] { "http://localhost:3000", "https://localhost:3000" })
                        .AllowCredentials();
                });
            });

            /** Configure IdentityServer Authentication **/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = _settings.Security.IdentityServer.AuthorityUrl;
                    options.ApiName = _settings.Security.IdentityServer.ApiName;
                    options.ApiSecret = _settings.Security.IdentityServer.ApiSecret;
                });
            

            /** Configure Swagger **/
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                var disco = GetIdentityServerDiscoveryDocument();

                /** Configure a Bearer token authentication **/
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             },
                             Scheme = JwtBearerDefaults.AuthenticationScheme,
                             Name = JwtBearerDefaults.AuthenticationScheme,
                             In = ParameterLocation.Header,

                         },
                         disco.ScopesSupported.ToList()
                     }
                 });

                /** Configure a OAuth2 with Password flow authentication **/
                c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Description ="OAuth authentication with a Bearer Authorization header.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(disco.AuthorizeEndpoint),
                            TokenUrl = new Uri(disco.TokenEndpoint)
                        }
                    },
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "OAuth2"
                            },
                            Scheme = "OAuth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                        },
                        disco.ScopesSupported.ToList()
                    }
                });
            });

            /** Register Session Manager **/
            services.AddScoped<ISessionManager, SessionManager>();

            _serviceStartUp.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPermission");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/hubs/chat");
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

        private DiscoveryDocumentResponse GetIdentityServerDiscoveryDocument()
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync(_settings.Security.IdentityServer.AuthorityUrl).Result;
            if (disco.IsError) 
                throw new Exception($"It was not possible to connect to the identity server. Error: {disco.Error}");
            return disco;
        }
    }
}
