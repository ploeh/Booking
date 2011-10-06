using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class CapacityTests
    {
        [Theory, AutoDomainData]
        public void RemainingIsCorrect([Frozen]int expected, Capacity sut)
        {
            Assert.Equal<int>(expected, sut.Remaining);
        }

        [Theory, AutoDomainData]
        public void CanReserveReturnsFalseWhenQuantityIsGreaterThanRemaining(
            Capacity sut, 
            RequestReservationCommand command)
        {
            var greaterQuantity = sut.Remaining + 1;
            var request = command.WithQuantity(greaterQuantity);

            bool actual = sut.CanReserve(request);

            Assert.False(actual);
        }

        [Theory, AutoDomainData]
        public void CanReserveReturnsTrueWhenQuantityIsEqualToRemaining(
            Capacity sut, 
            RequestReservationCommand command)
        {
            var request = command.WithQuantity(sut.Remaining);
            var actual = sut.CanReserve(request);
            Assert.True(actual);
        }

        [Theory, AutoDomainData]
        public void CanReserveReturnsTrueWhenQuantityIsLessThanRemaining(
            Capacity sut,
            RequestReservationCommand command)
        {
            var lesserQuantity = sut.Remaining - 1;
            var request = command.WithQuantity(lesserQuantity);

            var actual = sut.CanReserve(request);

            Assert.True(actual);
        }

        [Theory, AutoDomainData]
        public void ReserveThrowsWhenQuantityIsGreaterThanRemaining(
            Capacity sut, 
            RequestReservationCommand command)
        {
            var greaterQuantity = sut.Remaining + 1;
            var request = command.WithQuantity(greaterQuantity);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                sut.Reserve(request));
        }

        [Theory, AutoDomainData]
        public void ReserveDoesNotThrowWhenQuantityIsEqualToRemaining(
            Capacity sut,
            RequestReservationCommand command)
        {
            var request = command.WithQuantity(sut.Remaining);
            Assert.DoesNotThrow(() =>
                sut.Reserve(request));
        }

        [Theory, AutoDomainData]
        public void ReserveDoesNotThrowWhenQuantityIsLessThanRemaining(
            Capacity sut,
            RequestReservationCommand command)
        {
            var lesserQuantity = sut.Remaining - 1;
            var request = command.WithQuantity(lesserQuantity);
            Assert.DoesNotThrow(() =>
                sut.Reserve(request));
        }

        [Theory, AutoDomainData]
        public void ReserveReturnsInstanceWithCorrectlyDecrementedRemaining(
            int quantity,
            Capacity sut,
            RequestReservationCommand command)
        {
            var expected = sut.Remaining - quantity;
            var request = command.WithQuantity(quantity);

            Capacity actual = sut.Reserve(request);

            Assert.Equal(expected, actual.Remaining);
        }

        [Theory, AutoDomainData]
        public void SutIsEquatable(Capacity sut)
        {
            Assert.IsAssignableFrom<IEquatable<Capacity>>(sut);
        }

        [Theory, AutoDomainData]
        public void SutDoesNotEqualNull(Capacity sut)
        {
            var actual = BothEquals(sut, null);
            Assert.False(actual.Any(b => b));
        }

        [Theory, AutoDomainData]
        public void SutDoesNotEqualAnonymousOther(Capacity sut, object other)
        {
            Assert.False(sut.Equals(other));
        }

        [Theory, AutoDomainData]
        public void SutDoesNotEqualOtherWhenRemainingDiffers(Capacity sut, Capacity other)
        {
            var actual = BothEquals(sut, other);
            Assert.False(actual.Any(b => b));
        }

        [Theory, AutoDomainData]
        public void SutDoesNotEqualOtherWhenDifferentReservationsHaveBeenMade(
            [Frozen]int remaining,
            Capacity sut,
            Capacity other)
        {
            var actual = BothEquals(sut, other);
            Assert.False(actual.Any(b => b));
        }

        [Theory, AutoDomainData]
        public void SutEqualsOtherWhenBothReservationsAndRemainingAreEqual(
            [Frozen]int remaining,
            [Frozen]Guid[] ids,
            Capacity sut, 
            Capacity other)
        {
            var actual = BothEquals(sut, other);
            Assert.True(actual.All(b => b));
        }

        [Theory, AutoDomainData]
        public void GetHashCodeReturnsCorrectResult([Frozen]Guid[] ids, Capacity sut)
        {
            var expectedHashCode = ids
                .Select(g => g.GetHashCode())
                .Aggregate((x, y) => x ^ y)
                ^ sut.Remaining.GetHashCode();
            Assert.Equal(expectedHashCode, sut.GetHashCode());
        }

        private static IEnumerable<bool> BothEquals<T>(T sut, T other) where T : IEquatable<T>
        {
            yield return sut.Equals((object)other);
            yield return sut.Equals(other);
        }
    }
}
