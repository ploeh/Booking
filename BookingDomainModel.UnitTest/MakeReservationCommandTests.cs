using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;
using Moq;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class MakeReservationCommandTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(MakeReservationCommand sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }

        [Theory, AutoDomainData]
        public void DateIsCorrect([Frozen]DateTime expected, MakeReservationCommand sut)
        {
            Assert.Equal<DateTime>(expected, sut.Date);
        }

        [Theory, AutoDomainData]
        public void EmailIsCorrect([Frozen]string expected, MakeReservationCommand sut)
        {
            Assert.Equal<string>(expected, sut.Email);
        }

        [Theory, AutoDomainData]
        public void NameIsCorrect([Frozen]string expected, MakeReservationCommand sut)
        {
            Assert.Equal<string>(expected, sut.Name);
        }

        [Theory, AutoDomainData]
        public void QuantityIsCorrect([Frozen]int expected, MakeReservationCommand sut)
        {
            Assert.Equal<int>(expected, sut.Quantity);
        }

        [Theory, AutoDomainData]
        public void IdIsUnique(MakeReservationCommand sut, MakeReservationCommand other)
        {
            Assert.NotEqual(sut.Id, other.Id);
        }

        [Theory, AutoDomainData]
        public void IdIsStable(MakeReservationCommand sut)
        {
            Assert.Equal(sut.Id, sut.Id);
        }

        [Theory, AutoDomainData]
        public void DoesNotEqualAnonymousObject(MakeReservationCommand sut,
            object anonymousObject)
        {
            Assert.False(sut.Equals(anonymousObject));            
        }

        [Theory, AutoDomainData]
        public void EqualsWhenIdEquals(MakeReservationCommand sut,
            Mock<IMessage> messageStub)
        {
            messageStub.SetupGet(m => m.Id).Returns(sut.Id);
            var actual = BothEquals(sut, messageStub.Object);
            Assert.True(actual.All(b => b));
        }

        [Theory, AutoDomainData]
        public void DoesNotEqualWhenIdsDiffer(MakeReservationCommand sut,
            Mock<IMessage> messageStub)
        {
            messageStub.SetupGet(m => m.Id).Returns(Guid.NewGuid());
            var actual = BothEquals(sut, messageStub.Object);
            Assert.False(actual.Any(b => b));
        }

        [Theory, AutoDomainData]
        public void DoesNotEqualNull(MakeReservationCommand sut)
        {
            var actual = BothEquals<IMessage>(sut, null);
            Assert.False(actual.Any(b => b));
        }

        [Theory, AutoDomainData]
        public void GetHashCodeReturnsCorrectResult(MakeReservationCommand sut)
        {
            var actual = sut.GetHashCode();

            var expected = sut.Date.GetHashCode() ^
                sut.Email.GetHashCode() ^
                sut.Id.GetHashCode() ^
                sut.Name.GetHashCode() ^
                sut.Quantity.GetHashCode();
            Assert.Equal(expected, actual);
        }

        [Theory, AutoDomainData]
        public void EnvelopReturnsCorrectBody(MakeReservationCommand sut)
        {
            var actual = sut.Envelop();
            Assert.Equal(sut, actual.Body);
        }

        private static IEnumerable<bool> BothEquals<T>(T sut, T other) where T : IEquatable<T>
        {
            yield return sut.Equals((object)other);
            yield return sut.Equals(other);
        }
    }
}
