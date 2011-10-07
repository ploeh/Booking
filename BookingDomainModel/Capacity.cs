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

        public bool CanReserve(CapacityReservedEvent @event)
        {
            if (this.IsReplayOf(@event))
                return true;

            return this.remaining >= @event.Quantity;
        }

        public Capacity Reserve(CapacityReservedEvent @event)
        {
            if (!this.CanReserve(@event))
                throw new ArgumentOutOfRangeException("request", "The quantity must be less than or equal to the remaining quantity.");

            if (this.IsReplayOf(@event))
                return this;

            return new Capacity(this.remaining - @event.Quantity,
                this.acceptedReservations.Concat(new[] { @event.Id }).ToArray());
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

        public override int GetHashCode()
        {
            return this.remaining.GetHashCode()
                ^ this.acceptedReservations
                    .Select(g => g.GetHashCode())
                    .Aggregate((x, y) => x ^ y);
        }

        private bool IsReplayOf(IMessage message)
        {
            return this.acceptedReservations.Contains(message.Id);
        }
    }
}
