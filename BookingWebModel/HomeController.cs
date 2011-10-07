using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.WebModel
{
    public class HomeController : Controller
    {
        private readonly IReader<Month, IEnumerable<string>> reader;

        public HomeController(IReader<Month, IEnumerable<string>> reader)
        {
            this.reader = reader;
        }

        public ViewResult Get()
        {
            var now = DateTime.Now;
            var currentMonth = new Month(now.Year, now.Month);
            return this.View(this.reader.Query(currentMonth));
        }
    }
}