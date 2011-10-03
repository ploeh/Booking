using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.WebModel
{
    [DateViewModelBinder]
    public class DateViewModel
    {
        public DateViewModel()
        {
        }

        public DateViewModel(DateTime dateTime)
        {
            this.Year = dateTime.Year;
            this.Month = dateTime.Month;
            this.Day = dateTime.Day;
        }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public DateTime ToDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Day);
        }
    }
}
