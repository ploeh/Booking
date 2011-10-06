using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class PollingConsumer
    {
        private readonly IQueue queue;
        private readonly IObserver<Stream> observer;
        private readonly IEnumerable<Stream> streams;

        public PollingConsumer(IQueue queue, IObserver<Stream> observer)
        {
            this.queue = queue;
            this.observer = observer;
        }

        public void ConsumeAll()
        {
            foreach (var s in this.queue)
            {
                this.observer.OnNext(s);
                this.queue.Delete(s);
            }
        }
    }
}
