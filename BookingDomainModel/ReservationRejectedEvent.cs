using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class ReservationRejectedEvent : IMessage
    {
        private readonly Guid id;
        private readonly DateTime date;
        private readonly string name;
        private readonly string email;

        public ReservationRejectedEvent(Guid id, DateTime date, string name, string email)
        {
            this.id = id;
            this.date = date;
            this.name = name;
            this.email = email;
        }

        public Envelope Envelop()
        {
            throw new NotImplementedException();
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
    }
}
