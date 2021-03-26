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
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description ="JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

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
