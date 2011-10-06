using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityReservedEvent : IMessage
    {
        public Envelope Envelop()
        {
            throw new NotImplementedException();
        }

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }
    }
}
