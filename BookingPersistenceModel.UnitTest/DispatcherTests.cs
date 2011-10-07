using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.Samples.Booking.PersistenceModel;
using Xunit;
using Ploeh.AutoFixture.Xunit;
using Ploeh.Samples.Booking.DomainModel;
using Moq;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public abstract class DispatcherTests<T>
    {
        [Theory, AutoPersistenceData]
        public void SutIsObserverOfObject(Dispatcher<T> sut)
        {
            Assert.IsAssignableFrom<IObserver<object>>(sut);
        }

        [Theory, AutoPersistenceData]
        public void OnNextMathingValueConsumesConsumer(
            [Frozen]Mock<IConsumer<T>> consumerMock,
            Dispatcher<T> sut,
            T value)
        {
            sut.OnNext(value);
            consumerMock.Verify(c => c.Consume(value));
        }

        [Theory, AutoPersistenceData]
        public void OnNextObjectDoesNotConsumeConsumer(
            [Frozen]Mock<IConsumer<T>> consumerMock,
            Dispatcher<T> sut,
            object value)
        {
            sut.OnNext(value);
            consumerMock.Verify(c => c.Consume(It.IsAny<T>()), Times.Never());
        }
    }

    public class TypeMatchingConsumerTestsOfString : DispatcherTests<string> { }
    public class TypeMatchingConsumerTestsOfInt : DispatcherTests<int> { }
    public class TypeMatchingConsumerTestsOfGuid : DispatcherTests<Guid> { }
    public class TypeMatchingConsumerTestsOfVersion : DispatcherTests<Version> { }
}
