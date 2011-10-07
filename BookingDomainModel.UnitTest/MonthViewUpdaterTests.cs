using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class MonthViewUpdaterTests
    {
        [Theory, AutoDomainData]
        public void SutIsConsumer(MonthViewUpdater sut)
        {
            Assert.IsAssignableFrom<IConsumer<SoldOutEvent>>(sut);
        }
    }
}
