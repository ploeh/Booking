using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public interface IMessage : IEquatable<IMessage>
    {
        Envelope Envelop();

        Guid Id { get; }
    }
}
