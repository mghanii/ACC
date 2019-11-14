using ACC.Common.Extensions;
using ACC.Services.Tracking.Dto;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Queries
{
    public class VehicleQueries : IVehicleQueries
    {
        private readonly ITrackedVehicleRepository _vehicleRepository;
        private readonly ILogger _logger;

        public VehicleQueries(ITrackedVehicleRepository vehicleRepository, ILogger<VehicleQueries> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TrackedVehicleDto>> GetVehiclesAsync(GetVehiclesQuery query)
        {
            var vehicles = await _vehicleRepository.GetAsync(e => (query.Status == null || e.Status == query.Status)
                                && (string.IsNullOrEmpty(query.Customer) || e.CustomerName.Contains(query.Customer)))
                                    .AnyContext();

            return vehicles.Select(v =>
            {
                return new TrackedVehicleDto(v.Id, v.RegNr, v.Status, v.CustomerId, v.CustomerName, v.CustomerAddress);
            });
        }
    }
}