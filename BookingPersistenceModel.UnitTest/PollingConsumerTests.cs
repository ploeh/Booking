using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Ploeh.Samples.Booking.PersistenceModel;
using Xunit;
using System.IO;
using Moq;

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

        [Theory, AutoPersistenceData]
        public void EmptyQueueDispatchesAllStreams(
            [Frozen]IEnumerable<Stream> streams,
            [Frozen]Mock<IStreamConsumer> consumerMock,
            PollingConsumer sut)
        {
            sut.EmptyQueue();

            streams.ToList().ForEach(s =>
                consumerMock.Verify(c =>
                    c.Consume(s)));
        }
    }
}
