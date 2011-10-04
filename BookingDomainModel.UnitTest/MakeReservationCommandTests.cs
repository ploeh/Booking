using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class MakeReservationCommandTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(MakeReservationCommand sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }

        [Theory, AutoDomainData]
        public void DateIsCorrect([Frozen]DateTime expected, MakeReservationCommand sut)
        {
            Assert.Equal<DateTime>(expected, sut.Date);
        }

        [Theory, AutoDomainData]
        public void EmailIsCorrect([Frozen]string expected, MakeReservationCommand sut)
        {
            Assert.Equal<string>(expected, sut.Email);
        }

        [Theory, AutoDomainData]
        public void NameIsCorrect([Frozen]string expected, MakeReservationCommand sut)
        {
            Assert.Equal<string>(expected, sut.Name);
        }

        [Theory, AutoDomainData]
        public void QuantityIsCorrect([Frozen]int expected, MakeReservationCommand sut)
        {
            Assert.Equal<int>(expected, sut.Quantity);
        }

        [Theory, AutoDomainData]
        public void IdIsUnique(MakeReservationCommand sut, MakeReservationCommand other)
        {
            Assert.NotEqual(sut.Id, other.Id);
        }

        [Theory, AutoDomainData]
        public void IdIsStable(MakeReservationCommand sut)
        {
            Assert.Equal(sut.Id, sut.Id);
        }

        [Theory, AutoDomainData]
        public void DoesNotEqualAnonymousObject(MakeReservationCommand sut,
            object anonymousObject)
        {
            Assert.False(sut.Equals(anonymousObject));            
        }
    }
}
