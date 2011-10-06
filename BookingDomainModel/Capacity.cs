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
            return this.remaining >= request.Quantity;
        }

        public void Reserve(RequestReservationCommand request)
        {
            if (!this.CanReserve(request))
                throw new ArgumentOutOfRangeException("request", "The quantity must be less than or equal to the remaining quantity.");
        }
    }
}
