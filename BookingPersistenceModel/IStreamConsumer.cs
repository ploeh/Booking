using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public interface IStreamConsumer
    {
        void Consume(Stream stream);
    }
}
