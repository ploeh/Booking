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
    public class SoldOutEventTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(SoldOutEvent sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }

        [Theory, AutoDomainData]
        public void IdIsCorrect([Frozen]Guid expected, SoldOutEvent sut)
        {
            Assert.Equal(expected, sut.Id);
        }
    }
}
