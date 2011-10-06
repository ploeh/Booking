using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public interface IConsumer<T>
    {
        void Consume(T item);
    }
}
