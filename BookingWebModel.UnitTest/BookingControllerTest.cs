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
        public void GetReturnsCorrectModelType(BookingController sut,
            DateViewModel id)
        {
            ViewResult actual = sut.Get(id);
            Assert.IsAssignableFrom<BookingViewModel>(actual.Model);
        }

        [Theory, AutoWebData]
        public void GetReturnModelWithCorrectDate(BookingController sut,
            DateViewModel id)
        {
            var actual = sut.Get(id);

            var expected = id.ToDateTime();
            var model = Assert.IsAssignableFrom<BookingViewModel>(actual.Model);
            Assert.Equal(expected, model.Date);
        }

        [Theory, AutoWebData]
        public void PostReturnsCorrectViewName(BookingController sut,
            BookingViewModel model)
        {
            ViewResult actual = sut.Post(model);
            Assert.Equal("Receipt", actual.ViewName);
        }

        [Theory, AutoWebData]
        public void PostReturnsCorrectModel(BookingController sut,
            BookingViewModel expected)
        {
            var actual = sut.Post(expected);
            Assert.Equal(expected, actual.Model);
        }
    }
}
