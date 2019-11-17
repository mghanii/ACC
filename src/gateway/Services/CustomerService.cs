using ACC.ApiGateway.Dto;
using ACC.Common.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACC.ApiGateway.Services
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

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var content = await _httpClient.GetStringAsync("customers")
                      .AnyContext(); ;

            return JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(content);
        }
    }
}