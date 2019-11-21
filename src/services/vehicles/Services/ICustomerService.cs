using ACC.Services.Vehicles.Dto;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Services
{
    public interface ICustomerService
    {
        Task<VehicleOwnerDto> GetAsync(string id);
    }
}