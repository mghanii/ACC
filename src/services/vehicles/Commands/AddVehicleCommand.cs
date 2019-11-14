using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Vehicles.Commands
{
    public class AddVehicleCommand : ICommand
    {
        public string Id { get; }
        public string RegNr { get; }
        public string Brand { get; }
        public string Color { get; }
        public string Model { get; }
        public string Description { get; }
        public string CustomerId { get; }

        [JsonConstructor]
        public AddVehicleCommand(string id,
            string regNr,
           string brand,
           string color,
           string model,
           string description,
           string customerId)
        {
            Id = id;
            RegNr = regNr;
            Brand = brand;
            Color = color;
            Model = model;
            Description = description;
            CustomerId = customerId;
        }
    }
}