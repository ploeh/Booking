using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.Samples.Booking.DomainModel;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class SoldOutEventTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(SoldOutEvent sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }
    }
}
