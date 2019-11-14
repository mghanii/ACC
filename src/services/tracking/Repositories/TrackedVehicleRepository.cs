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

        public async Task<TrackedVehicle> GetAsync(string id)
        {
            return await _repository.GetAsync(id)
                .AnyContext();
        }

        public async Task<IEnumerable<TrackedVehicle>> GetAsync(Expression<Func<TrackedVehicle, bool>> predicate)
        {
            return await _repository.GetAsync(predicate)
                     .AnyContext();
        }

        public async Task AddAsync(TrackedVehicle vehicle)
        {
            await _repository.AddAsync(vehicle)
                      .AnyContext();
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id)
                            .AnyContext();
        }

        public async Task UpdateAsync(TrackedVehicle vehicle)
        {
            await _repository.UpdateAsync(vehicle)
                             .AnyContext();
        }
    }
}