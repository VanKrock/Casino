using AutoMapper;
using Casino.Abstractions;
using Casino.DataAccess;
using Casino.Middlewares;
using Casino.Options;
using Casino.Services;
using Casino.UseCases;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Casino
{
    /// <summary>
    /// Entry point for ASP.NET Core app.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Entry point for web application.
        /// </summary>
        /// <param name="configuration">Global configuration.</param>
        /// <param name="environment">Environment.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration for web application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure application services on startup.
        /// </summary>
        /// <param name="services">Services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddMvc()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("Casino"));
            services.AddAutoMapper(typeof(CasinoMappingProfile));
            services.AddMediatR(typeof(Startup).Assembly);

            services.Configure<ShuffleOptions>(Configuration.GetSection("Shuffle"));
            services.Configure<CustomShuffleOptions>(Configuration.GetSection("Shuffle:Custom"));

            services.AddScoped<FisherYatesShaffleDeckService>();
            services.AddScoped<CustomShuffleDeckService>();
            services.AddScoped<IShuffleDeckService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ShuffleOptions>>();
                return options.Value.Algorithm switch
                {
                    ShuffleAlgorithm.Custom => sp.GetRequiredService<CustomShuffleDeckService>(),
                    _ => sp.GetRequiredService<FisherYatesShaffleDeckService>()
                };
            });

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Casino API",
                    Description = "Test task",
                    Contact = new OpenApiContact
                    {
                        Name = "Boris Vandyshev",
                    }
                });
            });
        }

        /// <summary>
        /// Configure web application.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "Casino API V1");
                });
            }

            app.UseMiddleware<DomainExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
