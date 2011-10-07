using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.SemanticComparison.Fluent;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class ReservationAcceptedEventQuickeningTests
    {
        [Theory, AutoDomainData]
        public void SutIsQuickening(ReservationAcceptedEvent.Quickening sut)
        {
            Assert.IsAssignableFrom<IQuickening>(sut);
        }

        [Theory, AutoDomainData]
        public void QuickenReturnsCorrectResultWhenBodyTypeMatches(
            ReservationAcceptedEvent.Quickening sut,
            ReservationAcceptedEvent @event)
        {
            dynamic envelope = @event.Envelop();

            IEnumerable<IMessage> actual = sut.Quicken(envelope);

            var single = Assert.IsAssignableFrom<ReservationAcceptedEvent>(actual.Single());
            @event.AsSource().OfLikeness<ReservationAcceptedEvent>().ShouldEqual(single);
        }
    }
}
