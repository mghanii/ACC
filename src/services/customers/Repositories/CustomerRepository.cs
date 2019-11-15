using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Common.Repository;
using ACC.Services.Customers.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IRepository<Customer, string> _repository;
        private readonly IEventBus _eventBus;

        public CustomerRepository(IRepository<Customer, string> repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Customer> GetAsync(string id)
        {
            return await _repository.GetAsync(id)
                .AnyContext();
        }

        public async Task AddAsync(Customer customer)
        {
            await _repository.AddAsync(customer)
                 .AnyContext();
        }
    }
}