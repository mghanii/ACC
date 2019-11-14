using ACC.Common.Extensions;
using ACC.Services.Tracking.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerService> _logger;

        public VehicleService(HttpClient httpClient, ILogger<CustomerService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<VehicleDto> GetAsync(string id)
        {
            var content = await _httpClient.GetStringAsync($"/api/vehicles/{id}")
                      .AnyContext(); ;

            return JsonConvert.DeserializeObject<VehicleDto>(content);
        }
    }
}