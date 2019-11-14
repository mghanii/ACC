using ACC.Services.Tracking.Dto;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Services
{
    public interface IVehicleService
    {
        Task<VehicleDto> GetAsync(string id);
    }
}