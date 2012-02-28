using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public interface ICapacityRepository
    {
        Maybe<Capacity> Read(DateTime date);

        void Append(DateTime date, CapacityReservedEvent capacityReserved);
    }
}
