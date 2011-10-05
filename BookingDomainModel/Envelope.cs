using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Envelope<T>
    {
        private readonly T body;
        private readonly string bodyType;
        private readonly string version;

        public Envelope(T body, string version)
        {
            this.body = body;
            this.version = version;
            this.bodyType = this.body.GetType().Name.ToLowerInvariant();
        }

        public Envelope(T body, string version, string bodyType)
        {
            this.bodyType = bodyType;
        }

        public T Body
        {
            get { return this.body; }
        }

        public string BodyType
        {
            get { return this.bodyType; }
        }

        public string Version
        {
            get { return this.version; }
        }
    }
}
