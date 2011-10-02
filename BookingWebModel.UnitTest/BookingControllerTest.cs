using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using System.Web.Mvc;
using Ploeh.Samples.Booking.WebUI.Controllers;
using Ploeh.Samples.Booking.WebUI.Models;

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
    }
}
