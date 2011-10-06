using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Envelope
    {
        private readonly object body;
        private readonly string bodyType;
        private readonly string version;

        public Envelope(object body, string version)
            : this(body, version, Envelope.CreateDefaultBodyTypeFor(body.GetType()))
        {
        }

        public Envelope(object body, string version, string bodyType)
        {
            this.body = body;
            this.version = version;
            this.bodyType = bodyType;
        }

        public object Body
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

        public static string CreateDefaultBodyTypeFor(Type type)
        {
            return type.Name.ToLowerInvariant();
        }
    }
}
