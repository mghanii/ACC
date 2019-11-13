using ACC.Common.Messaging;
using ACC.Messaging.RabbitMq;
using ACC.Persistence.Mongo;
using ACC.Services.Vehicles.Events;
using ACC.Services.Vehicles.Handlers;
using ACC.Services.Vehicles.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ACC.Services.Vehicles
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
            services.AddMvc();
            services.AddLogging();
            services.AddMongoDB(Configuration, "mongo");
            services.AddTransient(typeof(IEventHandler<VehicleStatusReportedEvent>), typeof(VehicleStatusReportedHandler));

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleHistoryRepository, VehicleHistoryRepository>();
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
                endpoints.MapControllers();
            });

            app.UseRabbitMq()
                .SubscribeEvent<VehicleStatusReportedEvent>("connectivity");
        }
    }
}