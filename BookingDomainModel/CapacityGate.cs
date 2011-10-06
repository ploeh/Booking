using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityGate : IConsumer<RequestReservationCommand>
    {
        private readonly IChannel<CapacityReservedEvent> capacityChannel;

        public CapacityGate(IChannel<CapacityReservedEvent> capacityChannel)
        {
            this.capacityChannel = capacityChannel;
        }

        public void Consume(RequestReservationCommand item)
        {
            this.capacityChannel.Send(item.ReserveCapacity());
        }
    }
}
