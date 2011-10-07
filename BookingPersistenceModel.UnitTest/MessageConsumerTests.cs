using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.Samples.Booking.PersistenceModel;
using Xunit;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public class MessageConsumerTests
    {
        [Theory, AutoPersistenceData]
        public void SutIsObserverOfObject(MessageConsumer sut)
        {
            Assert.IsAssignableFrom<IObserver<object>>(sut);
        }
    }
}
