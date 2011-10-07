using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public interface ICapacityRepository
    {
        IEnumerable<Capacity> Read(DateTime date);

        void Write(CapacityReservedEvent capacityReserved);
    }
}
