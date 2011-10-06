using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class PollingConsumer
    {
        private readonly IObserver<Stream> observer;
        private readonly IEnumerable<Stream> streams;

        public PollingConsumer(IEnumerable<Stream> streams, IObserver<Stream> observer)
        {
            this.observer = observer;
            this.streams = streams;
        }

        public void ConsumeSequence()
        {
            foreach (var s in this.streams)
                this.observer.OnNext(s);
        }
    }
}
