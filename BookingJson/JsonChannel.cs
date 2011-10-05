using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.JsonAntiCorruption
{
    public class JsonChannel<T> : IChannel<T> where T : IMessage
    {
        public void Send(T message)
        {
        }
    }
}
