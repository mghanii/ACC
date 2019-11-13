using ACC.Services.Vehicles.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetAsync(string id);

        Task AddAsync(Vehicle vehicle);

        Task UpdateAsync(Vehicle vehicle);

        Task DeleteAsync(string id);
    }
}