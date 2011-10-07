using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityGate : IConsumer<RequestReservationCommand>
    {
        private readonly ICapacityRepository repository;
        private readonly IChannel<ReservationAcceptedEvent> acceptChannel;
        private readonly IChannel<ReservationRejectedEvent> rejectChannel;
        private readonly IChannel<SoldOutEvent> soldOutChannel;

        public CapacityGate(ICapacityRepository repository,
            IChannel<ReservationAcceptedEvent> acceptChannel,
            IChannel<ReservationRejectedEvent> rejectChannel,
            IChannel<SoldOutEvent> soldOutChannel)
        {
            this.repository = repository;
            this.acceptChannel = acceptChannel;
            this.rejectChannel = rejectChannel;
            this.soldOutChannel = soldOutChannel;
        }

        public void Consume(RequestReservationCommand item)
        {
            var originalCapacity = this.repository.Read(item.Date.Date);
            if (originalCapacity.CanReserve(item))
            {
                var reservedCapacity = item.ReserveCapacity();
                var newCapacity = originalCapacity.Reserve(reservedCapacity);
                if (!newCapacity.Equals(originalCapacity))
                {
                    this.repository.Write(reservedCapacity);
                    this.acceptChannel.Send(item.Accept());
                    if (newCapacity.Remaining <= 0)
                        this.soldOutChannel.Send(new SoldOutEvent(item.Date.Date));
                }
            }
            else
                rejectChannel.Send(item.Reject());
        }
    }
}
