using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Samples.Booking.WebModel;
using Xunit.Extensions;
using Ploeh.AutoFixture.Idioms;

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

        [Theory, AutoWebData]
        public void YearIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<DateViewModel>.GetProperty<int>(sut => sut.Year));
        }
    }
}
