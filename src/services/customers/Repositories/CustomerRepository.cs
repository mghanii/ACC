using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Common.Repository;
using ACC.Services.Customers.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Services.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IRepository<Customer, string> _repository;
        private readonly IBusPublisher _busPublisher;

        public CustomerRepository(IRepository<Customer, string> repository, IBusPublisher busPublisher)
        {
            _repository = repository;
            _busPublisher = busPublisher;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _repository.GetAsync(_ => true)
                .AnyContext();
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