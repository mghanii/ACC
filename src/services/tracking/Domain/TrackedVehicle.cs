using ACC.Common.Types;
using System;

namespace ACC.Services.Tracking.Domain
{
    public class TrackedVehicle : EntityBase, IIdentifiable, IEquatable<TrackedVehicle>
    {
        public string IPAddress { get; private set; }
        public string RegNr { get; private set; }

        public string Status { get; private set; }
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
            Status = TrackedVehicleStatus.Disconnected;
        }

        public void SetConnectionStatus(string status)
        {
            Status = status;
            SetUpdateDate();
        }

        public bool Equals(TrackedVehicle other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TrackedVehicle);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TrackedVehicle left, TrackedVehicle right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TrackedVehicle left, TrackedVehicle right)
        {
            return !Equals(left, right);
        }
    }
}