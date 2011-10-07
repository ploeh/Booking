using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public interface IStoreReader<T>
    {
        IEnumerable<Stream> OpenStreamsFor(T item);
    }
}
