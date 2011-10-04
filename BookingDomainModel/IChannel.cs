using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public interface IChannel<in T> where T : IMessage
    {
        void Send(T message);
    }
}
