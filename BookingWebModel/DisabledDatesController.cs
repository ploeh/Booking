using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ploeh.Samples.Booking.WebModel
{
    public class DisabledDatesController : Controller
    {
        [OutputCache(Duration = 0, VaryByParam = "none")]
        public JsonResult Get(int year, int month)
        {
            return this.Json(new string[0], JsonRequestBehavior.AllowGet);
        }
    }
}