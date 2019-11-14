using ACC.Services.Tracking.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Repositories
{
    public interface ITrackedVehicleRepository
    {
        Task<TrackedVehicle> GetAsync(string id);

        Task<IEnumerable<TrackedVehicle>> GetAsync(Expression<Func<TrackedVehicle, bool>> predicate);

        Task AddAsync(TrackedVehicle vehicle);

        Task UpdateAsync(TrackedVehicle vehicle);

        Task DeleteAsync(string id);
    }
}