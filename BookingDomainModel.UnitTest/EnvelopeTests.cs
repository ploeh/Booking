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
    public class EnvelopeTests
    {
        [Theory, AutoDomainData]
        public void BodyIsCorrectWhenConstructedModestly(
            [Frozen]object expected,
            [Modest]Envelope sut)
        {
            var actual = sut.Body;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void BodyTypeIsCorrectWhenConstructedModestly(
            [Modest]Envelope sut)
        {
            string actual = sut.BodyType;
            var expected = sut.Body.GetType().Name.ToLowerInvariant();
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void VersionIsCorrectWhenConstructedModestly(
            [Frozen]string expected,
            [Modest]Envelope sut)
        {
            string actual = sut.Version;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void BodyTypeIsCorrectWhenConstructedGreedily(
            [Frozen]string expected,
            [Greedy]Envelope sut)
        {
            var actual = sut.BodyType;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void BodyIsCorrectWhenConstructedGreedily(
            [Frozen]object expected,
            [Greedy]Envelope sut)
        {
            var actual = sut.Body;
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void VersionIsCorrectWhenConstructedGreedily(
            [Frozen]string expected,
            [Greedy]Envelope sut)
        {
            var actual = sut.Version;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(typeof(object))]
        [InlineData(typeof(string))]
        [InlineData(typeof(int))]
        [InlineData(typeof(Guid))]
        [InlineData(typeof(DateTime))]
        [InlineData(typeof(Version))]
        public void CreateDefaultBodyTypeReturnsCorrectResult(Type type)
        {
            string actual = Envelope.CreateDefaultBodyTypeFor(type);
            
            var expected = type.Name.ToLowerInvariant();
            Assert.Equal(expected, actual);
        }
    }
}
