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
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public bool Deleted { get; private set; }

        public Vehicle(string id,
                       string regNr,
                       string color,
                       string brand,
                       string model,
                       string describtion,
                       string customerId,
                       string customerName)
              : base(id)
        {
            RegNr = regNr;
            Brand = brand;
            Color = color;
            Model = model;
            Description = describtion;
            CustomerId = customerId;
            CustomerName = customerName;
        }

        public void SetDeleteFlag()
        {
            Deleted = true;
            SetUpdateDate();
        }
    }
}