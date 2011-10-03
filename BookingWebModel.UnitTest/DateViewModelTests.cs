using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class DateViewModelTests
    {
        [Fact]
        public void SutHasDefaultConstructor()
        {
            Assert.DoesNotThrow(() =>
                new DateViewModel());
        }
    }
}
