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

            bool result = sut.CanReserve(request);

            Assert.False(result);
        }

        [Theory, AutoDomainData]
        public void CanReserveReturnsTrueWhenQuantityIsEqualToRemaining(
            Capacity sut, 
            RequestReservationCommand command)
        {
            var request = command.WithQuantity(sut.Remaining);
            var result = sut.CanReserve(request);
            Assert.True(result);
        }
    }
}
