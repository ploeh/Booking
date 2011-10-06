using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Ploeh.Samples.Booking.PersistenceModel;
using Xunit;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public class PollingConsumerTests
    {
        [Theory, AutoPersistenceData]
        public void ConsumerIsCorrect(
            [Frozen]IStreamConsumer expected,
            PollingConsumer sut)
        {
            IStreamConsumer actual = sut.Consumer;
            Assert.Equal(expected, actual);
        }
    }
}
