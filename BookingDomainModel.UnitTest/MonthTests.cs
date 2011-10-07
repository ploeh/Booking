using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Ploeh.Samples.Booking.DomainModel;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class MonthTests
    {
        [Theory, AutoDomainData]
        public void YearIsCorrect([Frozen]int expected, Month sut)
        {
            Assert.Equal<int>(expected, sut.Year);
        }
    }
}
