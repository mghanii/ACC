namespace ACC.Services.Tracking.Dto
{
    public class VehicleDto
    {
        public string Id { get; set; }
        public string RegNr { get; set; }
        public string OwnerId { get; set; }

        public VehicleDto(string vehicleId, string regnr, string ownerId)
        {
            Id = vehicleId;
            RegNr = regnr;
            OwnerId = ownerId;
        }
    }
}