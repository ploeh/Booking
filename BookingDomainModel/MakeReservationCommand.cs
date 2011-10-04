using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class MakeReservationCommand
    {
        private readonly DateTime date;
        private readonly string email;
        private readonly string name;

        public MakeReservationCommand(DateTime date, string email, string name)
        {
            this.date = date;
            this.email = email;
            this.name = name;
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
    }
}
