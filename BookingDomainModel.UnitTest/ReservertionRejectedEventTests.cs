using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;
using Xunit.Extensions;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class ReservertionRejectedEventTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(ReservationRejectedEvent sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }
    }
}
