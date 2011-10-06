using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class ReservationRejectedEvent : IMessage
    {
        private readonly Guid id;
        private readonly DateTime date;

        public ReservationRejectedEvent(Guid id, DateTime date)
        {
            this.id = id;
            this.date = date;
        }

        public Envelope Envelop()
        {
            throw new NotImplementedException();
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public DateTime Date
        {
            get { return this.date; }
        }
    }
}
