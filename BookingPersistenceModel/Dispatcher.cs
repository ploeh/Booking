using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class Dispatcher<T> : IObserver<object>
    {
        private readonly IConsumer<T> consumer;

        public Dispatcher(IConsumer<T> consumer)
        {
            this.consumer = consumer;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(object value)
        {
            this.consumer.Consume((T)value);
        }
    }
}
