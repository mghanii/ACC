using ACC.Common.Types;

namespace ACC.Services.Vehicles.Domain
{
    public class Vehicle : EntityBase, IIdentifiable
    {
        public string RegistrationNumber { get; private set; }
        public bool IsConnected { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Description { get; private set; }
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerAddress { get; private set; }

        public Vehicle(string id,
                       string registrationNumber,
                       string customerId,
                       string customerName,
                       string customerAddress)
              : base(id)
        {
            RegistrationNumber = registrationNumber;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
        }

        public void SetConnectionStatus(bool isConnected)
        {
            IsConnected = isConnected;
            SetUpdateDate();
        }
    }
}