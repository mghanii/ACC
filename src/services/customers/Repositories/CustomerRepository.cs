using ACC.Common.Extensions;
using ACC.Common.Repository;
using ACC.Services.Customers.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IRepository<Customer, string> _repository;

        public CustomerRepository(IRepository<Customer, string> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> GetAsync(string id)
        {
            return await _repository.GetAsync(id)
                .AnyContext();
        }
    }
}