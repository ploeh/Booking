using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityGate : IConsumer<RequestReservationCommand>
    {
        private readonly ICapacityRepository repository;
        private readonly IChannel<CapacityReservedEvent> capacityChannel;

        public CapacityGate(ICapacityRepository repository,
            IChannel<CapacityReservedEvent> capacityChannel)
        {
            this.repository = repository;
            this.capacityChannel = capacityChannel;
        }

        public void Consume(RequestReservationCommand item)
        {
            var originalCapacity = repository.Read(item.Date.Date);
            if (originalCapacity.CanReserve(item))
                this.capacityChannel.Send(item.ReserveCapacity());
        }
    }
}
