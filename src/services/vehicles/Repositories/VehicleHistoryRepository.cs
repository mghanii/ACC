using ACC.Common.Extensions;
using ACC.Common.Repository;
using ACC.Services.Vehicles.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Repositories
{
    public class VehicleHistoryRepository : IVehicleHistoryRepository
    {
        private readonly IRepository<VehicleHistory, string> _repository;

        public VehicleHistoryRepository(IRepository<VehicleHistory, string> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(VehicleHistory history)
        {
            await _repository.AddAsync(history)
                      .AnyContext();
        }

        public async Task<VehicleHistory> GetAsync(string id)
        {
            return await _repository.GetAsync(id)
                .AnyContext();
        }
    }
}