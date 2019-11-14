using ACC.Services.Vehicles.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Queries
{
    public interface IVehicleQueries
    {
        Task<IEnumerable<VehicleDto>> GetAsync(GetVehiclesQuery query);

        Task<VehicleDto> GetAsync(string id);
    }
}