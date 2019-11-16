using ACC.Services.Tracking.Domain;
using System;

namespace ACC.Services.Tracking.Dto
{
    public class TrackedVehicleDto : IEquatable<TrackedVehicleDto>
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

        public bool Equals(TrackedVehicleDto other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TrackedVehicleDto);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TrackedVehicleDto left, TrackedVehicleDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TrackedVehicleDto left, TrackedVehicleDto right)
        {
            return !Equals(left, right);
        }
    }
}