using ACC.Common.Messaging;
using ACC.Common.Repository;
using ACC.Messaging.RabbitMq;
using ACC.Persistence.Mongo;
using ACC.Services.Vehicles.Commands;
using ACC.Services.Vehicles.Domain;
using ACC.Services.Vehicles.Events;
using ACC.Services.Vehicles.Handlers;
using ACC.Services.Vehicles.Queries;
using ACC.Services.Vehicles.Repositories;
using ACC.Services.Vehicles.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ACC.Services.Vehicles
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
                client.BaseAddress = new Uri(Configuration["customer-service-url"]);
            });

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleQueries, VehicleQueries>();

            services.AddTransient(typeof(ICommandHandler<AddVehicleCommand>), typeof(AddVehicleHandler));

            services.AddRabbitMq(Configuration, "rabbitmq");
            services.AddMongoDB(Configuration, "mongo");
            services.AddMongoRepository<Vehicle>("vehicles");
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

            app.UseRabbitMq();
        }
    }
}