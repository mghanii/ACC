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
    public class TrackedVehiclesQueries : ITrackedVehiclesQueries
    {
        private readonly ITrackedVehicleRepository _trackedVehicleRepository;
        private readonly ILogger<TrackedVehiclesQueries> _logger;

        public TrackedVehiclesQueries(ITrackedVehicleRepository vehicleRepository, ILogger<TrackedVehiclesQueries> logger)
        {
            _trackedVehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TrackedVehicleDto>> GetAsync(GetTrackedVehiclesQuery query)
        {
            var status = query?.Status?.ToLower();
            var customerId = query?.CustomerId?.ToLower();

            var vehicles = await _trackedVehicleRepository.GetAsync(e => (string.IsNullOrWhiteSpace(status) || e.Status == status)
                                                                      && (string.IsNullOrWhiteSpace(customerId) || e.CustomerId == customerId))
                                                           .AnyContext();

            return vehicles.Select(v =>
            {
                return new TrackedVehicleDto(v.Id, v.RegNr, v.Status, v.CustomerId, v.CustomerName, v.CustomerAddress);
            });
        }
    }
}