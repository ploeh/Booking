using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class TypeMatchingConsumer<T> : IObserver<object>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(object value)
        {
        }
    }
}
