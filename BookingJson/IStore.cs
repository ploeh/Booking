using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.JsonAntiCorruption
{
    public interface IStore<T>
    {
        Stream GetStreamFor(T item);
    }
}
