using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityGate : ICommandHandler<RequestReservationCommand>
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

        public void Execute(RequestReservationCommand command)
        {
            var originalCapacity = this.repository.Read(command.Date.Date)
                .DefaultIfEmpty(Capacity.Default)
                .Single();

            var reservedCapacity = command.ReserveCapacity();
            if (originalCapacity.CanApply(reservedCapacity))
            {                
                var newCapacity = originalCapacity.Apply(reservedCapacity);
                if (!newCapacity.Equals(originalCapacity))
                {
                    this.repository.Append(command.Date.Date, reservedCapacity);

                    this.acceptChannel.Send(command.Accept());
                    if (newCapacity.Remaining <= 0)
                        this.soldOutChannel.Send(new SoldOutEvent(command.Date.Date));
                }
            }
            else
                rejectChannel.Send(command.Reject());
        }
    }
}
