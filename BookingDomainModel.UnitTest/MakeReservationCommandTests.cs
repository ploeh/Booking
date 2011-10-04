using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class MakeReservationCommandTests
    {
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
    }
}
