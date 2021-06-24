using ClayTest.API.Infrastructure;
using ClayTest.API.Infrastructure.Middleware;
using ClayTest.Application.Services;
using ClayTest.Application.Services.Interfaces;
using ClayTest.DataAccess.Infrastructure;
using ClayTest.DataAccess.Repositories;
using ClayTest.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ClayTest.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddControllers();
            services.AddHealthChecks();
            services.ConfigureServicesSwagger();

            services.Configure<ConnectionOptions>(Configuration.GetSection(nameof(ConnectionOptions)));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IClaimService, ClaimService>();
            services.AddTransient<IDoorsService, DoorsService>();
            services.AddTransient<IDoorEventLogsService, DoorEventLogsService>();

            services.AddTransient<IDoorsRepository, DoorsRepository>();
            services.AddTransient<IDoorEventLogsRepository, DoorEventLogsRepository>();

            services.AddJwt(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClayTest.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseMiddleware<PerformanceMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
