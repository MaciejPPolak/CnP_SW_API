using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SW_API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;
using SW_API.Domain.Interfaces;
using SW_API.Infrastructure.Repositories;
using SW_API.Server.Services;
using Newtonsoft.Json;

namespace SW_API
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                );
            });

            // DB Access

            services.AddScoped<ICharacterRepository, CharacterRepository>();

            // Server logic

            services.AddScoped<ICharacterService, CharacterService>();

            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<SWDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database"),
                        x => x.MigrationsAssembly("SW_API.Infrastructure")));

            AddSwagger(services);

            services.AddOptions();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);

            app.UseSwagger();

            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "Star Wars Character API");
                conf.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Code and Pepper - Interview Task",
                    Description = "Star Wars Character API",
                    Contact = new OpenApiContact
                    {
                        Name = "Maciej Polak",
                        Email = "maciejppolak@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "LIC",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);

                config.CustomSchemaIds(i => i.FullName);
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<SWDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
