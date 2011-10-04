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
    }
}
