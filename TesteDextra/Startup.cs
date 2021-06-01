using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TesteDextra.Repositories;
using TesteDextra.Repositories.Interfaces;
using TesteDextra.Services;
using TesteDextra.Services.Interfaces;

namespace TesteDextra
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Dependency Injection
            // Configurations
            services.AddSingleton(Configuration);

            // Repositories
            services.AddSingleton<ICharactersRepository, CharactersRepository>();
            services.AddSingleton<IHousesRepository, HousesRepository>();

            // Services
            services.AddSingleton<ICharactersService, CharactersService>();
            services.AddSingleton<IHousesService, HousesService>();

            #endregion

            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Potter Api",
                    Description = "Potter Api - A test API to Dextra",
                    Contact = new OpenApiContact
                    {
                        Name = "David Chipana",
                        Email = "chipana_david@hotmail.com",
                        Url = new Uri("https://github.com/chipana"),
                    }
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PotterApi");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
