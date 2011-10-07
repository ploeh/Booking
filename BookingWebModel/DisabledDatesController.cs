using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.WebModel
{
    public class DisabledDatesController : Controller
    {
        private readonly IReader<Month, IEnumerable<string>> reader;

        public DisabledDatesController(IReader<Month, IEnumerable<string>> reader)
        {
            this.reader = reader;
        }

        [OutputCache(Duration = 0, VaryByParam = "none")]
        public JsonResult Get(int year, int month)
        {
            var disabledDates = reader.Query(new Month(year, month));
            return this.Json(disabledDates.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}