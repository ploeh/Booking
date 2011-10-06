using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityReservedEvent : IMessage
    {
        private readonly Guid id;

        public CapacityReservedEvent(Guid id)
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
