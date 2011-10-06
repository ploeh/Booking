using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;
using System.Dynamic;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class RequestReservationCommandQuickeningTests
    {
        [Theory, AutoDomainData]
        public void SutIsQuickening(RequestReservationCommand.Quickening sut)
        {
            Assert.IsAssignableFrom<IQuickening>(sut);
        }

        [Theory, AutoDomainData]
        public void QuickenReturnsCorrectResultWhenBodyTypeMatches(
            RequestReservationCommand.Quickening sut,
            RequestReservationCommand command)
        {
            dynamic envelope = command.Envelop();

            IEnumerable<IMessage> actual = sut.Quicken(envelope);

            var single = Assert.IsAssignableFrom<RequestReservationCommand>(actual.Single());
            Assert.Equal(command.Email, single.Email);
            Assert.Equal(command.Id, single.Id);
            Assert.Equal(command.Name, single.Name);
            Assert.Equal(command.Quantity, single.Quantity);
        }

        [Theory, AutoDomainData]
        public void QuickenReturnsNothingWhenBodyTypeDoesNotMatch(
            RequestReservationCommand.Quickening sut,
            string bodyType)
        {
            dynamic envelope = new ExpandoObject();
            envelope.BodyType = bodyType;

            var actual = sut.Quicken(envelope);

            Assert.Empty(actual);
        }
    }
}
