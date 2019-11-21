using ACC.Common.Extensions;
using ACC.Services.Vehicles.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(HttpClient httpClient, ILogger<CustomerService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<VehicleOwnerDto> GetAsync(string id)
        {
            var content = await _httpClient.GetStringAsync($"/api/customers/{id}")
                      .AnyContext(); ;

            return JsonConvert.DeserializeObject<VehicleOwnerDto>(content);
        }
    }
}