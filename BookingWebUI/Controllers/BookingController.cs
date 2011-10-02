using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ploeh.Samples.Booking.WebUI.Models;

namespace Ploeh.Samples.Booking.WebUI.Controllers
{
    public class BookingController : Controller
    {
        public ViewResult Make(int year, int month, int day)
        {
            return this.View(new BookingViewModel { Date = new DateTime(year, month, day) });
        }
    }
}