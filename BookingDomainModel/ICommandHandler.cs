using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public interface ICommandHandler<T>
    {
        void Execute(T command);
    }
}
