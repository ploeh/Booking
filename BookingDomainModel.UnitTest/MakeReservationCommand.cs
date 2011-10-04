using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class MakeReservationCommand
    {
        private readonly DateTime date;

        public MakeReservationCommand(DateTime date)
        {
            this.date = date;
        }

        public DateTime Date
        {
            get { return this.date; }
        }
    }
}
