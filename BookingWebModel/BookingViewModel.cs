using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ploeh.Samples.Booking.WebModel
{
    public class BookingViewModel
    {
        public DateTime Date { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int RemainingCapacity { get; set; }
    }
}