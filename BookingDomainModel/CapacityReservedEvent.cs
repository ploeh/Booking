using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class CapacityReservedEvent : IMessage
    {
        private readonly Guid id;
        private readonly int quantity;

        public CapacityReservedEvent(Guid id, int quantity)
        {
            this.id = id;
            this.quantity = quantity;
        }

        public Envelope Envelop()
        {
            throw new NotImplementedException();
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public int Quantity
        {
            get { return this.quantity; }
        }
    }
}
