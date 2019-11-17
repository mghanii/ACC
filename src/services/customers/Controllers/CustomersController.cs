using ACC.Common.Extensions;
using ACC.Services.Customers.Domain;
using ACC.Services.Customers.Dto;
using ACC.Services.Customers.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

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

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var customers = await _customerRepository.GetAllAsync()
                .AnyContext();

            var dtos = customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = $"{c.Address.Line1}, {c.Address.PostCode} {c.Address.City} {c.Address.Country}"
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerDto>> GetById(string id)
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
                Address = address
            };

            return Ok(dto);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AddCustomerDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<AddCustomerDto>> Create([FromBody]AddCustomerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dto.Id = Guid.NewGuid().ToString();

            var customer = new Customer(dto.Id, dto.Name);
            customer.SetAddress(dto.AddressLine1, dto.AddressLine2, dto.City, dto.State, dto.Country, dto.PostCode);

            await _customerRepository.AddAsync(customer)
                .AnyContext();

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
    }
}