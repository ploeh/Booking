using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class PollingConsumer
    {
        private readonly IStreamConsumer consumer;
        private readonly IEnumerable<Stream> streams;

        public PollingConsumer(IEnumerable<Stream> streams, IStreamConsumer consumer)
        {
            this.consumer = consumer;
            this.streams = streams;
        }

        public IStreamConsumer Consumer
        {
            get { return this.consumer; }
        }

        public void EmptyQueue()
        {
            foreach (var s in this.streams)
                this.consumer.Consume(s);
        }
    }
}
