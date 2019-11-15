using ACC.Common.Messaging;
using ACC.Common.Ping;
using ACC.Common.Repository;
using ACC.Messaging.RabbitMq;
using ACC.Persistence.Mongo;
using ACC.Services.Tracking.Commands;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Handlers;
using ACC.Services.Tracking.Options;
using ACC.Services.Tracking.Queries;
using ACC.Services.Tracking.Repositories;
using ACC.Services.Tracking.Scheduling;
using ACC.Services.Tracking.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ACC.Services.Tracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddLogging();

            services.AddHttpClient<ICustomerService, CustomerService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["customers-service-url"]);
            });

            services.AddHttpClient<ICustomerService, CustomerService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["vehicles-service-url"]);
            });

            services.Configure<TrackingOptions>(Configuration.GetSection("trackingSettings"));

            services.AddRabbitMq(Configuration, "rabbitmq");
            services.AddMongoDB(Configuration, "mongo");

            services.AddMongoRepository<TrackedVehicle>("vehicles");
            services.AddMongoRepository<TrackingHistory>("history");

            services.AddSingleton<IPingSender>(new PingSenderSimulator());

            services.AddScoped<ITrackedVehicleRepository, TrackedVehicleRepository>();
            services.AddScoped<ITrackingHistoryRepository, TrackingHistoryRepository>();
            services.AddScoped<ITrackedVehiclesQueries, TrackedVehiclesQueries>();

            services.AddTransient(typeof(IEventHandler<VehicleDeletedEvent>), typeof(VehicleDeletedHandler));
            services.AddTransient(typeof(ICommandHandler<TrackVehicleCommand>), typeof(TrackVehicleHandler));
            services.AddTransient(typeof(ICommandHandler<StopVehicleTrackingCommand>), typeof(StopVehicleTrackingHandler));

            services.AddHostedService<VehiclesTrackingService>();
        }

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

            app.ApplicationServices.GetService<IDbInitializer>().InitializeAsync();

            app.UseRabbitMq()
                .SubscribeCommand<TrackVehicleCommand>("")
                .SubscribeCommand<StopVehicleTrackingCommand>("")
                .SubscribeEvent<VehicleDeletedEvent>("vehicles");
        }
    }
}