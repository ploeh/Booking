using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public interface IQuickening
    {
        IEnumerable<IMessage> Quicken(dynamic envelope);
    }
}
