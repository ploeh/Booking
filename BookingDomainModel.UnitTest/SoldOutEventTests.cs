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
        public void IdIsUnique(SoldOutEvent sut, SoldOutEvent other)
        {
            Assert.NotEqual(sut.Id, other.Id);
        }

        [Theory, AutoDomainData]
        public void IdIsStable(SoldOutEvent sut)
        {
            Assert.Equal(sut.Id, sut.Id);
        }

        [Theory, AutoDomainData]
        public void DateIsCorrect([Frozen]DateTime date, SoldOutEvent sut)
        {
            Assert.Equal<DateTime>(date, sut.Date);
        }

        [Theory, AutoDomainData]
        public void EnvelopReturnsCorrectBody(SoldOutEvent sut)
        {
            var actual = sut.Envelop();
            Assert.Equal(sut, actual.Body);
        }
    }
}
