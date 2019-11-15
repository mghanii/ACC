using ACC.Services.Tracking.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Repositories
{
    public interface ITrackedVehicleRepository
    {
        Task<TrackedVehicle> GetAsync(string vehicleId);

        Task<IEnumerable<TrackedVehicle>> GetAsync(Expression<Func<TrackedVehicle, bool>> predicate);

        Task AddAsync(TrackedVehicle trackedVehicle);

        Task UpdateAsync(TrackedVehicle trackedVehicle);

        Task DeleteAsync(string vehicleId);

        Task<bool> ExistsAsync(string vehicleId);
    }
}