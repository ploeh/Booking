using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public interface IQueue : IEnumerable<Stream>
    {
        void Delete(Stream stream);
    }
}
