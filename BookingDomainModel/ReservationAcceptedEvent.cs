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
        private readonly string email;
        private readonly int quantity;

        public ReservationAcceptedEvent(Guid id, DateTime date, string name, string email, int quantity)
        {
            this.id = id;
            this.date = date;
            this.name = name;
            this.email = email;
            this.quantity = quantity;
        }

        protected ReservationAcceptedEvent(dynamic source)
        {
            this.id = source.Id;
            this.date = source.Date;
            this.name = source.Name;
            this.email = source.Email;
            this.quantity = source.Quantity;
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

        public string Name
        {
            get { return this.name; }
        }

        public string Email
        {
            get { return this.email; }
        }

        public int Quantity
        {
            get { return this.quantity; }
        }

        public class Quickening : IQuickening
        {
            public IEnumerable<IMessage> Quicken(dynamic envelope)
            {
                if (envelope.BodyType != Envelope.CreateDefaultBodyTypeFor(typeof(ReservationAcceptedEvent)))
                    yield break;

                yield return new ReservationAcceptedEvent(envelope.Body);
            }
        }
    }
}
