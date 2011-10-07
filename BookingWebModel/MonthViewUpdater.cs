using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.WebModel
{
    public class MonthViewUpdater : IConsumer<SoldOutEvent>
    {
        private readonly IObserver<DateTime> disabler;

        public MonthViewUpdater(IObserver<DateTime> disabler)
        {
            this.disabler = disabler;
        }

        public void Consume(SoldOutEvent item)
        {
            this.disabler.OnNext(item.Date);
        }
    }
}
