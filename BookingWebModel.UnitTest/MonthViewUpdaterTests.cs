using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class MonthViewUpdaterTests
    {
        [Theory, AutoWebData]
        public void SutIsConsumer(MonthViewUpdater sut)
        {
            Assert.IsAssignableFrom<IConsumer<SoldOutEvent>>(sut);
        }
    }
}
