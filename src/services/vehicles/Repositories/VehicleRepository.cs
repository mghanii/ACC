using ACC.Common.Extensions;
using ACC.Common.Repository;
using ACC.Services.Vehicles.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IRepository<Vehicle, string> _repository;

        public VehicleRepository(IRepository<Vehicle, string> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _repository.AddAsync(vehicle)
                      .AnyContext();
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id)
                            .AnyContext();
        }

        public async Task<Vehicle> GetAsync(string id)
        {
            return await _repository.GetAsync(id)
                .AnyContext();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            await _repository.UpdateAsync(vehicle)
                             .AnyContext();
        }
    }
}