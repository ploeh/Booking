using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class ReservationRejectedEvent : IMessage
    {
        private readonly Guid id;

        public ReservationRejectedEvent(Guid id)
        {
            this.id = id;
        }

        public Envelope Envelop()
        {
            throw new NotImplementedException();
        }

        public Guid Id
        {
            get { return this.id; }
        }
    }
}
