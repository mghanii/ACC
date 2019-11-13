using ACC.Common.Extensions;
using ACC.Common.Repository;
using ACC.Services.VehicleConnectivity.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Services.VehicleConnectivity.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IRepository<Vehicle, string> _repository;

        public VehicleRepository(IRepository<Vehicle, string> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            return await _repository.GetAsync(_ => true)
               .AnyContext();
        }
    }
}