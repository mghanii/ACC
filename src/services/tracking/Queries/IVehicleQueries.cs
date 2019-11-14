using ACC.Services.Tracking.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Queries
{
    public interface IVehicleQueries
    {
        Task<IEnumerable<TrackedVehicleDto>> GetVehiclesAsync(GetVehiclesQuery query);
    }
}