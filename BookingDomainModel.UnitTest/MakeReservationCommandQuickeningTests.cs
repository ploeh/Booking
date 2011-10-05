using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class MakeReservationCommandQuickeningTests
    {
        [Theory, AutoDomainData]
        public void QuickenReturnsCorrectResultWhenBodyTypeMatches(
            MakeReservationCommand.Quickening sut,
            MakeReservationCommand command)
        {
            dynamic envelope = command.Envelop();

            IEnumerable<IMessage> actual = sut.Quicken(envelope);

            var single = Assert.IsAssignableFrom<MakeReservationCommand>(actual.Single());
            Assert.Equal(command.Email, single.Email);
            Assert.Equal(command.Id, single.Id);
            Assert.Equal(command.Name, single.Name);
            Assert.Equal(command.Quantity, single.Quantity);
        }
    }
}
