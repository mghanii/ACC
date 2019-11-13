using ACC.Services.VehicleConnectivity.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Services.VehicleConnectivity.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllVehicles();
    }
}