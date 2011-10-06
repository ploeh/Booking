using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Capacity
    {
        private readonly int remaining;

        public Capacity(int remaining)
        {
            this.remaining = remaining;
        }

        public int Remaining
        {
            get { return this.remaining; }
        }

        public bool CanReserve(RequestReservationCommand request)
        {
            return false;
        }
    }
}
