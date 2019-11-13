using ACC.Services.Vehicles.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetAsync(string id);

        Task<IEnumerable<Vehicle>> GetAsync(Expression<Func<Vehicle, bool>> predicate);

        Task AddAsync(Vehicle vehicle);

        Task UpdateAsync(Vehicle vehicle);

        Task DeleteAsync(string id);
    }
}