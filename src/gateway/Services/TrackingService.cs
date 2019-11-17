using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ACC.ApiGateway.Dto;
using ACC.ApiGateway.Queries;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ACC.Common.Extensions;

namespace ACC.ApiGateway.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TrackingService> _logger;

        public TrackingService(HttpClient httpClient, ILogger<TrackingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<TrackedVehicleDto>> GetTrackedVehiclesAsync(GetTrackedVehiclesQuery query)
        {
            var path = $"tracked/vehicles?customerId={query.CustomerId}&status={query.Status}";

            var content = await _httpClient.GetStringAsync(path)
                      .AnyContext(); ;

            return JsonConvert.DeserializeObject<IEnumerable<TrackedVehicleDto>>(content);
        }
    }
}