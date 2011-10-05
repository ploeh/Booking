using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.AutoFixture.Xunit;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public abstract class EnvelopeTests<T>
    {
        [Theory, AutoDomainData]
        public void BodyIsCorrectWhenConstructedModestly([Frozen]T expected,
            [Modest]Envelope<T> sut)
        {
            T actual = sut.Body;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void BodyTypeIsCorrectWhenConstructedModestly(
            [Modest]Envelope<T> sut)
        {
            string actual = sut.BodyType;
            var expected = sut.Body.GetType().Name.ToLowerInvariant();
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void VersionIsCorrectWhenConstructedModestly(
            [Frozen]string expected,
            [Modest]Envelope<T> sut)
        {
            string actual = sut.Version;
            Assert.Equal(expected, actual);
        }
    }

    public class EnvelopeTestsOfObject : EnvelopeTests<object> { }
    public class EnvelopeTestsOfString : EnvelopeTests<string> { }
    public class EnvelopeTestsOfInt32 : EnvelopeTests<int> { }
    public class EnvelopeTestsOfGuid : EnvelopeTests<Guid> { }
    public class EnvelopeTestsOfDateTime : EnvelopeTests<DateTime> { }
    public class EnvelopeTestsOfVersion : EnvelopeTests<Version> { }
}
