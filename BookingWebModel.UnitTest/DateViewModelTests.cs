using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Samples.Booking.WebModel;
using Xunit.Extensions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.SemanticComparison.Fluent;

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

        [Theory, AutoWebData]
        public void MonthIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<DateViewModel>.GetProperty<int>(sut => sut.Month));
        }

        [Theory, AutoWebData]
        public void DayIsWritable(WritablePropertyAssertion assertion)
        {
            assertion.Verify(Reflect<DateViewModel>.GetProperty<int>(sut => sut.Day));
        }

        [Theory, AutoWebData]
        public void ToDateTimeReturnsCorrectResult(DateViewModel sut)
        {
            DateTime actual = sut.ToDateTime();
            var expected = new DateTime(sut.Year, sut.Month, sut.Day);
            Assert.Equal(expected, actual);
        }

        [Theory, AutoWebData]
        public void ConstructFromDateTimeReturnsCorrectSut(DateTime dateTime)
        {
            var actual = new DateViewModel(dateTime);

            dateTime.AsSource().OfLikeness<DateViewModel>().ShouldEqual(actual);
        }
    }
}
