using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.WebModel
{
    public class MonthViewUpdater : IEventHandler<SoldOutEvent>
    {
        private readonly IObserver<DateTime> disabler;

        public MonthViewUpdater(IObserver<DateTime> disabler)
        {
            this.disabler = disabler;
        }

        public void Handle(SoldOutEvent @event)
        {
            this.disabler.OnNext(@event.Date);
        }
    }
}
