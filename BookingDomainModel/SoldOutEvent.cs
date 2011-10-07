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

        protected SoldOutEvent(dynamic source)
        {
            this.id = source.Id;
            this.date = source.Date;
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

        public class Quickening : IQuickening
        {
            public IEnumerable<IMessage> Quicken(dynamic envelope)
            {
                if (envelope.BodyType != Envelope.CreateDefaultBodyTypeFor(typeof(SoldOutEvent)))
                    yield break;

                yield return new SoldOutEvent(envelope.Body);
            }
        }
    }
}
