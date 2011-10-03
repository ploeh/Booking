using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ploeh.Samples.Booking.WebModel
{
    public class BookingController : Controller
    {
        public ViewResult Get(DateViewModel id)
        {
            return this.View(new BookingViewModel { Date = id.ToDateTime() });
        }

        [HttpPost]
        public ViewResult Post(BookingViewModel model)
        {
            return this.View("Receipt", model);
        }
    }
}