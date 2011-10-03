using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using System.Web.Mvc;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class BookingControllerTest
    {
        [Theory, AutoWebData]
        public void SutIsController(BookingController sut)
        {
            Assert.IsAssignableFrom<IController>(sut);
        }

        [Theory, AutoWebData]
        public void MakeReturnsCorrectModelType(BookingController sut,
            int year,
            int month,
            int day)
        {
            ViewResult actual = sut.Make(year, month, day);
            Assert.IsAssignableFrom<BookingViewModel>(actual.Model);
        }

        [Theory, AutoWebData]
        public void MakeReturnModelWithCorrectDate(BookingController sut,
            int year,
            int month,
            int day)
        {
            var actual = sut.Make(year, month, day);

            var expected = new DateTime(year, month, day);
            var model = Assert.IsAssignableFrom<BookingViewModel>(actual.Model);
            Assert.Equal(expected, model.Date);
        }

        [Theory, AutoWebData]
        public void PostMakeReturnsCorrectViewName(BookingController sut,
            BookingViewModel model)
        {
            ViewResult actual = sut.Make(model);
            Assert.Equal("Receipt", actual.ViewName);
        }

        [Theory, AutoWebData]
        public void PostMakeReturnsCorrectModel(BookingController sut,
            BookingViewModel expected)
        {
            var actual = sut.Make(expected);
            Assert.Equal(expected, actual.Model);
        }
    }
}
