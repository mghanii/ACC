using ACC.Services.Vehicles.Domain;

namespace ACC.Services.Vehicles.Dto
{
    public class VehicleDto
    {
        public string Id { get; }
        public string RegNr { get; }
        public string Color { get; }
        public string Brand { get; }
        public string Model { get; }
        public string Description { get; }
        public string OwnerId { get; }
        public string OwnerName { get; }

        public VehicleDto(Vehicle vehicle)
        {
            Id = vehicle.Id;
            RegNr = vehicle.RegNr;
            Color = vehicle.Color;
            Brand = vehicle.Brand;
            Model = vehicle.Model;
            OwnerId = vehicle.OwnerId;
            OwnerName = vehicle.OwnerName;
        }
    }
}