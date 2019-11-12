using ACC.Common.Exceptions;
using ACC.Common.Types;
using System;

namespace ACC.Services.Customers.Domain
{
    public class Customer : EntityBase, IIdentifiable
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; } = new Address();

        public Customer(string id, string firstName, string lastName)
            : base(id)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new AccException("customer_first_name", "Customer first name can not be null or empty");
            }

            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new AccException("customer_last_name", "Customer last name can not be null or empty");
            }

            LastName = lastName;
        }

        public void SetAddress(string Line1, string Line2, string city, string state, string country, string postCode)
        {
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