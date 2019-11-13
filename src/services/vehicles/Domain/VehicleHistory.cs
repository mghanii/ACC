using ACC.Common.Types;

namespace ACC.Services.Vehicles.Domain
{
    public class VehicleHistory : EntityBase, IIdentifiable
    {
        public string VehicleId { get; }
        public bool IsConnected { get; }

        public VehicleHistory(string id, string vehicleId, bool isConnected)
            : base(id)
        {
            VehicleId = vehicleId;
            IsConnected = isConnected;
        }
    }
}