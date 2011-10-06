using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class RequestReservationCommand : IMessage
    {
        private readonly DateTime date;
        private readonly string email;
        private readonly string name;
        private readonly int quantity;
        private readonly Guid id;

        public RequestReservationCommand(DateTime date, string email, string name, int quantity)
        {
            this.date = date;
            this.email = email;
            this.name = name;
            this.quantity = quantity;
            this.id = Guid.NewGuid();
        }

        protected RequestReservationCommand(dynamic source)
        {
            this.date = source.Date;
            this.email = source.Email;
            this.name = source.Name;
            this.quantity = source.Quantity;
            this.id = source.Id;
        }

        public ReservationAcceptedEvent Accept()
        {
            return new ReservationAcceptedEvent(this.id, this.date, this.name, this.email, this.quantity);
        }

        public ReservationRejectedEvent Reject()
        {
            return new ReservationRejectedEvent(this.id, this.date, this.name, this.email, this.quantity);
        }

        public CapacityReservedEvent ReserveCapacity()
        {
            return new CapacityReservedEvent(this.id, this.quantity);
        }

        public RequestReservationCommand WithQuantity(int newQuantity)
        {
            return new RequestReservationCommand(this.date, this.email, this.name, newQuantity);
        }

        public Envelope Envelop()
        {
            return new Envelope(this, "1");
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public string Email
        {
            get { return this.email; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Quantity
        {
            get { return this.quantity; }
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public class Quickening : IQuickening
        {
            public IEnumerable<IMessage> Quicken(dynamic envelope)
            {
                if (envelope.BodyType != Envelope.CreateDefaultBodyTypeFor(typeof(RequestReservationCommand)))
                    yield break;

                yield return new RequestReservationCommand(envelope.Body);
            }
        }
    }
}
