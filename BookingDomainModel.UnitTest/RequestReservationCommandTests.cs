using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;
using Moq;
using Ploeh.SemanticComparison.Fluent;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class RequestReservationCommandTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(RequestReservationCommand sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }

        [Theory, AutoDomainData]
        public void DateIsCorrect([Frozen]DateTime expected, RequestReservationCommand sut)
        {
            Assert.Equal<DateTime>(expected, sut.Date);
        }

        [Theory, AutoDomainData]
        public void EmailIsCorrect([Frozen]string expected, RequestReservationCommand sut)
        {
            Assert.Equal<string>(expected, sut.Email);
        }

        [Theory, AutoDomainData]
        public void NameIsCorrect([Frozen]string expected, RequestReservationCommand sut)
        {
            Assert.Equal<string>(expected, sut.Name);
        }

        [Theory, AutoDomainData]
        public void QuantityIsCorrect([Frozen]int expected, RequestReservationCommand sut)
        {
            Assert.Equal<int>(expected, sut.Quantity);
        }

        [Theory, AutoDomainData]
        public void IdIsUnique(RequestReservationCommand sut, RequestReservationCommand other)
        {
            Assert.NotEqual(sut.Id, other.Id);
        }

        [Theory, AutoDomainData]
        public void IdIsStable(RequestReservationCommand sut)
        {
            Assert.Equal(sut.Id, sut.Id);
        }

        [Theory, AutoDomainData]
        public void EnvelopReturnsCorrectBody(RequestReservationCommand sut)
        {
            var actual = sut.Envelop();
            Assert.Equal(sut, actual.Body);
        }

        [Theory, AutoDomainData]
        public void EnvelopReturnsCorrectVersion(RequestReservationCommand sut)
        {
            var actual = sut.Envelop();
            Assert.Equal("1", actual.Version);
        }

        [Theory, AutoDomainData]
        public void WithQuantityReturnsCorrectResult(RequestReservationCommand sut,
            int newQuantity)
        {
            RequestReservationCommand actual = sut.WithQuantity(newQuantity);

            sut.AsSource().OfLikeness<RequestReservationCommand>()
                .With(d => d.Quantity).EqualsWhen((s, d) => d.Quantity == newQuantity)
                .Without(d => d.Id)
                .ShouldEqual(actual);
        }

        [Theory, AutoDomainData]
        public void ReserveCapacityReturnsCorrectResult(RequestReservationCommand sut)
        {
            CapacityReservedEvent actual = sut.ReserveCapacity();

            sut.AsSource().OfLikeness<CapacityReservedEvent>()
                .ShouldEqual(actual);
        }

        [Theory, AutoDomainData]
        public void AcceptReturnsCorrectResult(RequestReservationCommand sut)
        {
            ReservationAcceptedEvent actual = sut.Accept();

            sut.AsSource().OfLikeness<ReservationAcceptedEvent>().ShouldEqual(actual);
        }
    }
}
