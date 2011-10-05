using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class MakeReservationCommand : IMessage
    {
        private readonly DateTime date;
        private readonly string email;
        private readonly string name;
        private readonly int quantity;
        private readonly Guid id;

        public MakeReservationCommand(DateTime date, string email, string name, int quantity)
        {
            this.date = date;
            this.email = email;
            this.name = name;
            this.quantity = quantity;
            this.id = Guid.NewGuid();
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

        public bool Equals(IMessage other)
        {
            if (other == null)
                return false;

            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            var other = obj as IMessage;
            if (other != null)
            {
                return this.Equals(other);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.date.GetHashCode() ^
                this.email.GetHashCode() ^
                this.id.GetHashCode() ^ 
                this.name.GetHashCode() ^ 
                this.quantity.GetHashCode();

        }
    }
}
