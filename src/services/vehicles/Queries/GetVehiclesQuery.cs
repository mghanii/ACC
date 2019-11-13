using ACC.Common.Types;
using ACC.Services.Vehicles.Dto;
using System.Collections.Generic;

namespace ACC.Services.Vehicles.Queries
{
    public class GetVehiclesQuery : IQuery<IEnumerable<VehicleDto>>
    {
        public string Customer { get; set; }
        public string Status { get; set; }
    }
}