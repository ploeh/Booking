using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class EventDispatcher<T> : IObserver<object>
    {
        private readonly IEventHandler<T> eventHandler;

        public EventDispatcher(IEventHandler<T> eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(object value)
        {
            if (value is T)
                this.eventHandler.Handle((T)value);
        }
    }
}
