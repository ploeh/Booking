using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class MonthViewUpdater : IConsumer<SoldOutEvent>
    {
        public void Consume(SoldOutEvent item)
        {
        }
    }
}
