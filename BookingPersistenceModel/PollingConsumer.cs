using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class PollingConsumer
    {
        private readonly IStreamConsumer consumer;

        public PollingConsumer(IStreamConsumer consumer)
        {
            this.consumer = consumer;
        }

        public IStreamConsumer Consumer
        {
            get { return this.consumer; }
        }
    }
}
