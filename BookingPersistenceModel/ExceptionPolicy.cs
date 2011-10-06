using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public static class ExceptionPolicy
    {
        public static bool IsUnsafeToSuppress(this Exception e)
        {
            // Cheating by suppressing every caught exception. Don't do this in production code. 
            return false;
        }
    }
}
