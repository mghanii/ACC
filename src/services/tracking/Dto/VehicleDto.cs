using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Dto
{
    public class VehicleDto
    {
        public string Id { get; set; }
        public string RegNr { get; set; }
        public string CustomerId { get; set; }
        public VehicleDto(string vehicleId, string regnr)
        {
            Id = vehicleId;
            RegNr = regnr;
        }
    }
}