using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class SoldOutEvent : IMessage
    {
        private readonly Guid id;
        private readonly DateTime date;

        public SoldOutEvent(DateTime date)
        {
            this.id = Guid.NewGuid();
            this.date = date;
        }

        public Envelope Envelop()
        {
            return new Envelope(this, "1");
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
