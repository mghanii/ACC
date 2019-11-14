using ACC.Services.Vehicles.Dto;
using ACC.Services.Vehicles.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACC.Common.Extensions;

namespace ACC.Services.Vehicles.Queries
{
    public class VehicleQueries : IVehicleQueries
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger _logger;

        public VehicleQueries(IVehicleRepository vehicleRepository, ILogger<VehicleQueries> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<VehicleDto> GetAsync(string id)
        {
            var vehicle = await _vehicleRepository.GetAsync(id)
                           .AnyContext();

            if (vehicle == null)
            {
                return null;
            }

            var dto = new VehicleDto
            {
                Id = vehicle.Id,
                RegNr = vehicle.RegNr,
                Color = vehicle.Color,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                CustomerId = vehicle.CustomerId,
                CustomerName = vehicle.CustomerName
            };

            return dto;
        }

        public async Task<IEnumerable<VehicleDto>> GetAsync(GetVehiclesQuery query)
        {
            var vehicles = await _vehicleRepository.GetAsync(_ => true)
                                    .AnyContext();

            return vehicles.Select(v =>
            {
                return new VehicleDto
                {
                    Id = v.Id,
                    RegNr = v.RegNr,
                    Color = v.Color,
                    Brand = v.Brand,
                    Model = v.Model,
                    Description = v.Description,
                    CustomerId = v.CustomerId,
                    CustomerName = v.CustomerName
                };
            });
        }
    }
}