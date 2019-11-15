using ACC.ApiGateway.Events;
using ACC.Common.Messaging;
using System;
using System.Threading.Tasks;

namespace ACC.ApiGateway.Handlers
{
    public class VehicleStatusChangedHandler : IEventHandler<VehicleStatusChangedEvent>
    {
        public async Task HandleAsync(VehicleStatusChangedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}