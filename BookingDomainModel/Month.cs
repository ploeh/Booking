using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Booking.DomainModel
{
    public class Month
    {
        private readonly int year;

        public Month(int year)
        {
            this.year = year;
        }

        public int Year
        {
            get { return this.year; }
        }
    }
}
