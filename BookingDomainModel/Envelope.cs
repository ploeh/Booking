using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Envelope<T>
    {
        private readonly T body;
        private readonly string version;

        public Envelope(T body, string version)
        {
            this.body = body;
            this.version = version;
        }

        public T Body
        {
            get { return this.body; }
        }

        public string MessageType
        {
            get { return this.body.GetType().Name.ToLowerInvariant(); }
        }

        public string Version
        {
            get { return this.version; }
        }
    }
}
