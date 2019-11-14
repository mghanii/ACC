using ACC.Services.Tracking.Dto;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetAsync(string id);
    }
}