using ACC.ApiGateway.Dto;
using ACC.Common.Types;
using System.Collections.Generic;

namespace ACC.ApiGateway.Queries
{
    public class GetTrackedVehiclesQuery : IQuery<IEnumerable<TrackedVehicleDto>>
    {
        public string CustomerId { get; set; }
        public string Status { get; set; }
    }
}