using ACC.Common.Types;

namespace ACC.Services.Tracking.Domain
{
    public class TrackedVehicle : EntityBase, IIdentifiable
    {
        public string IPAddress { get; private set; }
        public string RegNr { get; private set; }
        public TrackedVehicleStatus Status { get; private set; }
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerAddress { get; private set; }

        public TrackedVehicle(string id,
                       string ipAddress,
                       string regNr,
                       string customerId,
                       string customerName,
                       string customerAddress)
              : base(id)
        {
            IPAddress = ipAddress;
            RegNr = regNr;
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
        }

        public void SetConnectionStatus(TrackedVehicleStatus status)
        {
            Status = status;
            SetUpdateDate();
        }
    }
}