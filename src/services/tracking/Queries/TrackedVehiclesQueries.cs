using ACC.Common.Extensions;
using ACC.Services.Tracking.Domain;
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
        private readonly ILogger _logger;

        public TrackedVehiclesQueries(ITrackedVehicleRepository vehicleRepository, ILogger<TrackedVehiclesQueries> logger)
        {
            _trackedVehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TrackedVehicleDto>> GetAsync(GetTrackedVehiclesQuery query)
        {
            // var vehicles = await _trackedVehicleRepository.GetAsync(e => (query.Status == null || e.Status == query.Status)
            //                        && (string.IsNullOrEmpty(query.CustomerId) || e.CustomerId.Equals(query.CustomerId, StringComparison.OrdinalIgnoreCase)));

            var vehicles = Enumerable.Empty<TrackedVehicle>();

            if (query == null)
            {
                vehicles = await _trackedVehicleRepository.GetAsync(_ => true)
                                       .AnyContext();
            }
            else if (query.Status != null && !string.IsNullOrEmpty(query.CustomerId))
            {
                vehicles = await _trackedVehicleRepository.GetAsync(t =>
                                t.CustomerId.Equals(query.CustomerId, StringComparison.OrdinalIgnoreCase)
                                && t.Status == query.Status)
                              .AnyContext();
            }
            else if (!string.IsNullOrEmpty(query.CustomerId))
            {
                vehicles = await _trackedVehicleRepository.GetAsync(t =>
                                t.CustomerId.Equals(query.CustomerId, StringComparison.OrdinalIgnoreCase))
                                   .AnyContext();
            }
            else
            {
                vehicles = await _trackedVehicleRepository.GetAsync(t => t.Status == query.Status)
                              .AnyContext();
            }

            return vehicles.Select(v =>
            {
                return new TrackedVehicleDto(v.Id, v.RegNr, v.Status, v.CustomerId, v.CustomerName, v.CustomerAddress);
            });
        }
    }
}