using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Samples.Booking.WebUI.Models;
using Ploeh.AutoFixture.Idioms;
using Xunit.Extensions;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class BookingViewModelTests
    {
        [Fact]
        public void SutHasDefaultConstructor()
        {
            Assert.DoesNotThrow(() => 
                new BookingViewModel());
        }

        [Theory, AutoWebData]
        public void DateIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<BookingViewModel>.GetProperty<DateTime>(sut => sut.Date));
        }
    }
}
