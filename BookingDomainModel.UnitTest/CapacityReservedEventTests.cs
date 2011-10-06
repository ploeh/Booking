using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.Samples.Booking.DomainModel;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class CapacityReservedEventTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(CapacityReservedEvent sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }
    }
}
