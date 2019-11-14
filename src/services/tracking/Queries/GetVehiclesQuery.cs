using ACC.Common.Types;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Dto;
using System.Collections.Generic;

namespace ACC.Services.Tracking.Queries
{
    public class GetVehiclesQuery : IQuery<IEnumerable<TrackedVehicleDto>>
    {
        public string Customer { get; set; }
        public TrackedVehicleStatus? Status { get; set; }
    }
}