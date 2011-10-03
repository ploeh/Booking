using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.WebModel
{
    public interface IReader<in T, out TResult>
    {
        TResult Query(T arg);
    }
}
