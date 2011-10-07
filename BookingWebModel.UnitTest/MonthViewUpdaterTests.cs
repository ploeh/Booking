using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.AutoFixture.Xunit;
using Moq;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class MonthViewUpdaterTests
    {
        [Theory, AutoWebData]
        public void SutIsConsumer(MonthViewUpdater sut)
        {
            Assert.IsAssignableFrom<IConsumer<SoldOutEvent>>(sut);
        }

        [Theory, AutoWebData]
        public void ConsumeCorrectlyUpdatesViewStore(
            [Frozen]Mock<IObserver<DateTime>> observerMock,
            MonthViewUpdater sut,
            SoldOutEvent @event)
        {
            sut.Consume(@event);
            observerMock.Verify(s => s.OnNext(@event.Date));
        }
    }
}
