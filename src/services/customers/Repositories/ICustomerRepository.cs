using ACC.Services.Customers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Services.Customers.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(string id);

        Task<IEnumerable<Customer>> GetAllAsync();

        Task AddAsync(Customer customer);
    }
}