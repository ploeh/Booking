using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class ReservationAcceptedEvent : IMessage
    {
        private readonly Guid id;
        private readonly DateTime date;
        private readonly string name;

        public ReservationAcceptedEvent(Guid id, DateTime date, string name)
        {
            this.id = id;
            this.date = date;
            this.name = name;
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

        public string Name
        {
            get { return this.name; }
        }
    }
}
