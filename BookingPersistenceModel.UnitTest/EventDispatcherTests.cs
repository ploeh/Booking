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
    public abstract class EventDispatcherTests<T>
    {
        [Theory, AutoPersistenceData]
        public void SutIsObserverOfObject(EventDispatcher<T> sut)
        {
            Assert.IsAssignableFrom<IObserver<object>>(sut);
        }

        [Theory, AutoPersistenceData]
        public void OnNextMathingValueConsumesConsumer(
            [Frozen]Mock<IEventHandler<T>> consumerMock,
            EventDispatcher<T> sut,
            T value)
        {
            sut.OnNext(value);
            consumerMock.Verify(c => c.Handle(value));
        }

        [Theory, AutoPersistenceData]
        public void OnNextObjectDoesNotConsumeConsumer(
            [Frozen]Mock<IEventHandler<T>> consumerMock,
            EventDispatcher<T> sut,
            object value)
        {
            sut.OnNext(value);
            consumerMock.Verify(c => c.Handle(It.IsAny<T>()), Times.Never());
        }
    }

    public class TypeMatchingEventDispatcherTestsOfString : EventDispatcherTests<string> { }
    public class TypeMatchingEventDispatcherTestsOfInt : EventDispatcherTests<int> { }
    public class TypeMatchingEventDispatcherTestsOfGuid : EventDispatcherTests<Guid> { }
    public class TypeMatchingEventDispatcherTestsOfVersion : EventDispatcherTests<Version> { }
}
