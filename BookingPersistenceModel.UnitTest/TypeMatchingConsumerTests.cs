using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.Samples.Booking.PersistenceModel;
using Xunit;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public abstract class TypeMatchingConsumerTests<T>
    {
        [Theory, AutoPersistenceData]
        public void SutIsObserverOfObject(TypeMatchingConsumer<T> sut)
        {
            Assert.IsAssignableFrom<IObserver<object>>(sut);
        }
    }

    public class TypeMatchingConsumerTestsOfString : TypeMatchingConsumer<string> { }
    public class TypeMatchingConsumerTestsOfInt : TypeMatchingConsumer<int> { }
    public class TypeMatchingConsumerTestsOfGuid : TypeMatchingConsumer<Guid> { }
    public class TypeMatchingConsumerTestsOfVersion : TypeMatchingConsumer<Version> { }
}
