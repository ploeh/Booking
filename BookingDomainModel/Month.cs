using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Month
    {
        private readonly int year;
        private readonly int month;

        public Month(int year, int month)
        {
            this.year = year;
            this.month = month;
        }

        public int Year
        {
            get { return this.year; }
        }

        public int MonthNumber
        {
            get { return this.month; }
        }
    }
}
