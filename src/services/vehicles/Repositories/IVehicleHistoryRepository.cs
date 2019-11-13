using ACC.Services.Vehicles.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Repositories
{
    public interface IVehicleHistoryRepository
    {
        Task<VehicleHistory> GetAsync(string id);

        Task AddAsync(VehicleHistory history);
    }
}