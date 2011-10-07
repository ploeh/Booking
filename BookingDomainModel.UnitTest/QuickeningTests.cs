using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.SemanticComparison.Fluent;

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
    }

    public class QuickeningTestsOfReservationRejectedEvent : QuickeningTests<ReservationRejectedEvent.Quickening, ReservationRejectedEvent> { }
}
