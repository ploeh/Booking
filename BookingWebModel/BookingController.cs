using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ploeh.Samples.Booking.WebModel
{
    public class BookingController : Controller
    {
        public ViewResult Get(int year, int month, int day)
        {
            return this.View(new BookingViewModel { Date = new DateTime(year, month, day) });
        }

        [HttpPost]
        public ViewResult Make(BookingViewModel model)
        {
            return this.View("Receipt", model);
        }
    }
}