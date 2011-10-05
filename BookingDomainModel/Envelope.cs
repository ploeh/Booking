using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Envelope<T>
    {
        private readonly T body;

        public Envelope(T body)
        {
            this.body = body;
        }

        public T Body
        {
            get { return this.body; }
        }
    }
}
