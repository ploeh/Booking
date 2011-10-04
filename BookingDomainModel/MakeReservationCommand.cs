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
    }
}
