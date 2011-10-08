using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.Daemon
{
    public class CompositeObserver<T> : IObserver<T>
    {
        private readonly IEnumerable<IObserver<T>> observers;

        public CompositeObserver(params IObserver<T>[] observers)
        {
            this.observers = observers;
        }

        public CompositeObserver(IEnumerable<IObserver<T>> observers)
            : this(observers.ToArray())
        {
        }

        public void OnCompleted()
        {
            foreach (var o in this.observers)
                o.OnCompleted();
        }

        public void OnError(Exception error)
        {
            foreach (var o in this.observers)
                o.OnError(error);
        }

        public void OnNext(T value)
        {
            foreach (var o in this.observers)
                o.OnNext(value);
        }
    }
}
