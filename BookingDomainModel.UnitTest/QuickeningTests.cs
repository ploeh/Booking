using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.SemanticComparison.Fluent;
using System.Dynamic;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public abstract class QuickeningTests<TQuickening, TMessage>
        where TQuickening : IQuickening
        where TMessage : IMessage
    {
        [Theory, AutoDomainData]
        public void QuickenReturnsCorrectResultWhenBodyTypeMatches(
            TQuickening sut,
            TMessage message)
        {
            dynamic envelope = message.Envelop();

            IEnumerable<IMessage> actual = sut.Quicken(envelope);

            var single = Assert.IsAssignableFrom<TMessage>(actual.Single());
            message.AsSource().OfLikeness<TMessage>().ShouldEqual(single);
        }

        [Theory, AutoDomainData]
        public void QuickenReturnsNothingWhenBodyTypeDoesNotMatch(
            TQuickening sut,
            string bodyType)
        {
            dynamic envelope = new ExpandoObject();
            envelope.BodyType = bodyType;

            var actual = sut.Quicken(envelope);

            Assert.Empty(actual);
        }
    }

    public class QuickeningTestsOfRequestReservationCommand : QuickeningTests<RequestReservationCommand.Quickening, RequestReservationCommand> { }
    public class QuickeningTestsOfReservationAcceptedEvent : QuickeningTests<ReservationAcceptedEvent.Quickening, ReservationAcceptedEvent> { }
    public class QuickeningTestsOfReservationRejectedEvent : QuickeningTests<ReservationRejectedEvent.Quickening, ReservationRejectedEvent> { }
    public class QuickeningTestsOfCapacityReservedEvent : QuickeningTests<CapacityReservedEvent.Quickening, CapacityReservedEvent> { }
    public class QuickeningTestsOfSoldOutEvent : QuickeningTests<SoldOutEvent.Quickening, SoldOutEvent> { }
}
