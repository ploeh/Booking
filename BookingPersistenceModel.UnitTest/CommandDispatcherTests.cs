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
    public abstract class CommandDispatcherTests<T>
    {
        [Theory, AutoPersistenceData]
        public void SutIsObserverOfObject(CommandDispatcher<T> sut)
        {
            Assert.IsAssignableFrom<IObserver<object>>(sut);
        }

        [Theory, AutoPersistenceData]
        public void OnNextMathingValueConsumesConsumer(
            [Frozen]Mock<ICommandHandler<T>> consumerMock,
            CommandDispatcher<T> sut,
            T value)
        {
            sut.OnNext(value);
            consumerMock.Verify(c => c.Execute(value));
        }

        [Theory, AutoPersistenceData]
        public void OnNextObjectDoesNotConsumeConsumer(
            [Frozen]Mock<ICommandHandler<T>> consumerMock,
            CommandDispatcher<T> sut,
            object value)
        {
            sut.OnNext(value);
            consumerMock.Verify(c => c.Execute(It.IsAny<T>()), Times.Never());
        }
    }

    public class TypeMatchingCommandDispatcherTestsOfString : CommandDispatcherTests<string> { }
    public class TypeMatchingCommandDispatcherTestsOfInt : CommandDispatcherTests<int> { }
    public class TypeMatchingCommandDispatcherTestsOfGuid : CommandDispatcherTests<Guid> { }
    public class TypeMatchingCommandDispatcherTestsOfVersion : CommandDispatcherTests<Version> { }
}
