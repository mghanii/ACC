using ACC.Services.Customers.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ACC.Common.Extensions;
using Microsoft.Extensions.Logging;
using System;
using ACC.Services.Customers.Dto;

namespace ACC.Services.Customers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _logger;

        public CustomersController(ICustomerRepository customerRepository, ILogger<CustomersController> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _customerRepository.GetAsync(id)
                .AnyContext();

            if (customer == null)
            {
                _logger.LogInformation($"customer '{id}' was not found");

                return NotFound();
            }

            var address = "";

            if (customer.Address != null)
            {
                address = $"{customer.Address.Line1}, {customer.Address.PostCode} {customer.Address.City} {customer.Address.Country}";
            }

            var dto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Address = address
            };

            return Ok(dto);
        }
    }
}