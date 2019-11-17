using ACC.ApiGateway.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.ApiGateway.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
    }
}