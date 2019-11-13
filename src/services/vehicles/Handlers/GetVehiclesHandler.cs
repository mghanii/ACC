using ACC.Common.Extensions;
using ACC.Common.Types;
using ACC.Services.Vehicles.Dto;
using ACC.Services.Vehicles.Queries;
using ACC.Services.Vehicles.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Handlers
{
    public class GetVehiclesHandler : IQueryHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleDto>> HandleAsync(GetVehiclesQuery query)
        {
            bool? connected = null;

            if (query.Status.Equals("connected", StringComparison.OrdinalIgnoreCase))
            {
                connected = true;
            }
            else if (query.Status.Equals("disconnected", StringComparison.OrdinalIgnoreCase))
            {
                connected = false;
            }

            var vehicles = await _vehicleRepository.GetAsync(e => (connected == null || e.IsConnected == connected)
                                && (string.IsNullOrEmpty(query.Customer) || e.CustomerName.Contains(query.Customer)))
                                    .AnyContext();

            return vehicles.Select(v =>
            {
                return new VehicleDto(v.Id, v.RegistrationNumber, v.IsConnected, v.CustomerId, v.CustomerName, v.CustomerAddress);
            });
        }
    }
}