using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.Samples.Booking.DomainModel;
using Xunit;
using Ploeh.AutoFixture.Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class CapacityReservedEventTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(CapacityReservedEvent sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }

        [Theory, AutoDomainData]
        public void IdIsCorrect([Frozen]Guid expected, CapacityReservedEvent sut)
        {
            Assert.Equal(expected, sut.Id);
        }

        [Theory, AutoDomainData]
        public void QuantityIsCorrect([Frozen]int expected, CapacityReservedEvent sut)
        {
            int actual = sut.Quantity;
            Assert.Equal(expected, actual);
        }
    }
}
