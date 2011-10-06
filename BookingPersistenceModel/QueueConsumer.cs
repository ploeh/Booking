using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class QueueConsumer
    {
        private readonly IQueue queue;
        private readonly IObserver<Stream> observer;

        public QueueConsumer(IQueue queue, IObserver<Stream> observer)
        {
            this.queue = queue;
            this.observer = observer;
        }

        public void ConsumeAll()
        {
            foreach (var s in this.queue)
            {
                try
                {
                    this.observer.OnNext(s);
                    this.queue.Delete(s);
                }
                catch (Exception e)
                {
                    if (e.IsUnsafeToSuppress())
                        throw;
                }
            }
        }
    }
}
