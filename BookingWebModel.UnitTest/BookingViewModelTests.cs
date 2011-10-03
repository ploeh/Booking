using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
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

        [Theory, AutoWebData]
        public void EmailIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<BookingViewModel>.GetProperty<string>(sut => sut.Email));
        }

        [Theory, AutoWebData]
        public void NameIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<BookingViewModel>.GetProperty<string>(sut => sut.Name));
        }

        [Theory, AutoWebData]
        public void QuantityIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<BookingViewModel>.GetProperty<int>(sut => sut.Quantity));
        }

        [Theory, AutoWebData]
        public void RemainingCapacityIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<BookingViewModel>.GetProperty<int>(sut => sut.RemainingCapacity));
        }
    }
}
