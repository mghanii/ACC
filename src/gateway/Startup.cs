using ACC.ApiGateway.Events;
using ACC.ApiGateway.Handlers;
using ACC.ApiGateway.Services;
using ACC.Common.Messaging;
using ACC.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ACC.ApiGateway
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
            services.AddControllers();
            services.AddMvc();
            services.AddLogging();

            services.AddHttpClient<ITrackingService, TrackingService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["trackingServiceUrl"]);
            });

            services.AddScoped<IEventHandler<VehicleStatusChangedEvent>, VehicleStatusChangedHandler>();
            services.AddRabbitMq(Configuration, "rabbitmq");
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseRabbitMq()
              .SubscribeEvent<VehicleStatusChangedEvent>("tracking");
        }
    }
}