namespace ACC.Services.Tracking.Dto
{
    public class VehicleDto
    {
        public string Id { get; set; }
        public string RegNr { get; set; }
        public string CustomerId { get; set; }

        public VehicleDto(string vehicleId, string regnr, string customerId)
        {
            Id = vehicleId;
            RegNr = regnr;
            CustomerId = customerId;
        }
    }
}