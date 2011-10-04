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

        public MakeReservationCommand(DateTime date, string email)
        {
            this.date = date;
            this.email = email;
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public string Email
        {
            get { return this.email; }
        }
    }
}
