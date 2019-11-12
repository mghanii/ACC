using ACC.Common.Types;

namespace ACC.Services.Vehicles.Domain
{
    public class Vehicle : EntityBase, IIdentifiable
    {
        public string RegistrationNumber { get; }

        public Vehicle(string id, string registrationNumber) : base(id)
        {
            RegistrationNumber = registrationNumber;
        }
    }
}