using ACC.Services.Tracking.Domain;

namespace ACC.Services.Tracking.Dto
{
    public class TrackedVehicleDto
    {
        public string Id { get; }
        public string RegNr { get; }
        public string Status { get; }
        public string CustomerId { get; }
        public string CustomerName { get; }
        public string CustomerAddress { get; }

        public TrackedVehicleDto(
          string id,
          string regnr,
          string status,
          string customerId,
          string customerName,
          string customerAddress)
        {
            Id = id;
            RegNr = regnr;
            Status = status;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
        }
    }
}