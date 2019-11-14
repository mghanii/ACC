using ACC.Common.Exceptions;
using ACC.Common.Types;

namespace ACC.Services.Customers.Domain
{
    public class Customer : EntityBase, IIdentifiable
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; } = new Address();

        public Customer(string id, string email, string name)
            : base(id)
        {
            Email = email;
            SetName(name);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new AccException("invalid_customer_name", "Customer name can not be null or empty");
            }
        }

        public void SetAddress(string Line1, string Line2, string city, string state, string country, string postCode)
        {
            // TODO: validate address

            Address.Line1 = Line1;
            Address.Line2 = Line2;
            Address.City = city;
            Address.State = state;
            Address.Country = country;
            Address.PostCode = postCode;

            SetUpdateDate();
        }
    }
}