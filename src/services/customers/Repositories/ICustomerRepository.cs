using ACC.Services.Customers.Domain;
using System.Threading.Tasks;

namespace ACC.Services.Customers.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(string id);
    }
}