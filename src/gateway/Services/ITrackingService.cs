using ACC.ApiGateway.Dto;
using ACC.ApiGateway.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.ApiGateway.Services
{
    public interface ITrackingService
    {
        Task<IEnumerable<TrackedVehicleDto>> GetTrackedVehiclesAsync(GetTrackedVehiclesQuery query);
    }
}