namespace ACC.Services.Vehicles.Dto
{
    public class VehicleDto
    {
        public string Id { get; }
        public string RegistrationNumber { get; }
        public bool IsConnected { get; }
        public string CustomerId { get; }
        public string CustomerName { get; }
        public string CustomerAddress { get; }

        public VehicleDto(
          string id,
          string registrationNumber,
          bool isConnected,
          string customerId,
          string customerName,
          string customerAddress)
        {
            Id = id;
            RegistrationNumber = registrationNumber;
            IsConnected = isConnected;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
        }
    }
}