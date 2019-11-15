using ACC.Common.Extensions;
using ACC.Common.Repository;
using ACC.Services.Tracking.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Repositories
{
    public class TrackedVehicleRepository : ITrackedVehicleRepository
    {
        private readonly IRepository<TrackedVehicle, string> _repository;

        public TrackedVehicleRepository(IRepository<TrackedVehicle, string> repository)
        {
            _repository = repository;
        }

        public async Task<TrackedVehicle> GetAsync(string vehicleId)
        {
            return await _repository.GetAsync(vehicleId)
                .AnyContext();
        }

        public async Task<IEnumerable<TrackedVehicle>> GetAsync(Expression<Func<TrackedVehicle, bool>> predicate)
        {
            return await _repository.GetAsync(predicate)
                     .AnyContext();
        }

        public async Task AddAsync(TrackedVehicle trackedVehicle)
        {
            await _repository.AddAsync(trackedVehicle)
                      .AnyContext();
        }

        public async Task DeleteAsync(string vehicleId)
        {
            await _repository.DeleteAsync(vehicleId)
                            .AnyContext();
        }

        public async Task UpdateAsync(TrackedVehicle trackedVehicle)
        {
            await _repository.UpdateAsync(trackedVehicle)
                             .AnyContext();
        }

        public async Task<bool> ExistsAsync(string vehicleId)
        {
            return await _repository.ExistsAsync(t => t.Id == vehicleId)
                              .AnyContext();
        }
    }
}