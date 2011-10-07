using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class ReservationRejectedEventQuickeningTests
    {
        [Theory, AutoDomainData]
        public void SutIsQuickening(ReservationRejectedEvent.Quickening sut)
        {
            Assert.IsAssignableFrom<IQuickening>(sut);
        }
    }
}
