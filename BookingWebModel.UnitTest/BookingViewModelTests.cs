using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Samples.Booking.WebUI.Models;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class BookingViewModelTests
    {
        [Fact]
        public void SutHasDefaultConstructor()
        {
            Assert.DoesNotThrow(() => 
                new BookingViewModel());
        }
    }
}
