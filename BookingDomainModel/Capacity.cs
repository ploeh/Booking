using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Capacity : IEquatable<Capacity>
    {
        private readonly int remaining;
        private readonly HashSet<Guid> acceptedReservations;

        public Capacity(int remaining, params Guid[] acceptedReservations)
        {
            this.remaining = remaining;
            this.acceptedReservations = new HashSet<Guid>(acceptedReservations);
        }

        public int Remaining
        {
            get { return this.remaining; }
        }

        public bool CanReserve(RequestReservationCommand request)
        {
            return this.remaining >= request.Quantity;
        }

        public Capacity Reserve(RequestReservationCommand request)
        {
            if (!this.CanReserve(request))
                throw new ArgumentOutOfRangeException("request", "The quantity must be less than or equal to the remaining quantity.");

            return new Capacity(this.remaining - request.Quantity);
        }

        public bool Equals(Capacity other)
        {
            if (other == null)
                return false;

            return this.remaining == other.remaining
                && this.acceptedReservations.SetEquals(other.acceptedReservations);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Capacity;
            if (other != null)
            {
                return this.Equals(other);
            }
            return base.Equals(obj);
        }
    }
}
