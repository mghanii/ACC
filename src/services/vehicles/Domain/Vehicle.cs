using ACC.Common.Types;

namespace ACC.Services.Vehicles.Domain
{
    public class Vehicle : EntityBase, IIdentifiable
    {
        public string RegNr { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public string Description { get; private set; }
        public string OwnerId { get; private set; }
        public string OwnerName { get; private set; }
        public bool Deleted { get; private set; }

        public Vehicle(string id,
                       string regNr,
                       string color,
                       string brand,
                       string model,
                       string describtion,
                       string ownerId,
                       string ownerName)
              : base(id)
        {
            RegNr = regNr;
            Brand = brand;
            Color = color;
            Model = model;
            Description = describtion;
            OwnerId = ownerId;
            OwnerName = ownerName;
        }

        public void SetDeleteFlag()
        {
            Deleted = true;
            SetUpdateDate();
        }
    }
}