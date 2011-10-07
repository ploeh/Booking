using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.WebModel
{
    public class MonthViewUpdater : IConsumer<SoldOutEvent>
    {
        public void Consume(SoldOutEvent item)
        {
        }
    }
}
