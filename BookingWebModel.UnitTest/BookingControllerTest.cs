using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using System.Web.Mvc;
using Ploeh.AutoFixture.Xunit;
using Moq;
using Ploeh.Samples.Booking.WebModel;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.SemanticComparison.Fluent;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class BookingControllerTest
    {
        [Theory, AutoWebData]
        public void SutIsController(BookingController sut)
        {
            Assert.IsAssignableFrom<IController>(sut);
        }

        [Theory, AutoWebData]
        public void GetReturnsCorrectModelType(BookingController sut,
            DateViewModel id)
        {
            ViewResult actual = sut.Get(id);
            Assert.IsAssignableFrom<BookingViewModel>(actual.Model);
        }

        [Theory, AutoWebData]
        public void GetReturnModelWithCorrectDate(BookingController sut,
            DateViewModel id)
        {
            var actual = sut.Get(id);

            var expected = id.ToDateTime();
            var model = Assert.IsAssignableFrom<BookingViewModel>(actual.Model);
            Assert.Equal(expected, model.Date);
        }

        [Theory, AutoWebData]
        public void GetReturnsModelWithCorrectRemainingCapacity(
            [Frozen]Mock<IReader<DateTime, int>> readerStub,
            BookingController sut,
            DateViewModel id,
            int expected)
        {
            readerStub
                .Setup(r => r.Query(id.ToDateTime()))
                .Returns(expected);

            var actual = sut.Get(id);

            var model = Assert.IsAssignableFrom<BookingViewModel>(actual.Model);
            Assert.Equal(expected, model.RemainingCapacity);
        }

        [Theory, AutoWebData]
        public void PostReturnsCorrectViewName(BookingController sut,
            BookingViewModel model)
        {
            ViewResult actual = sut.Post(model);
            Assert.Equal("Receipt", actual.ViewName);
        }

        [Theory, AutoWebData]
        public void PostReturnsCorrectModel(BookingController sut,
            BookingViewModel expected)
        {
            var actual = sut.Post(expected);
            Assert.Equal(expected, actual.Model);
        }

        [Theory, AutoWebData]
        public void PostSendsOnChannel(
            [Frozen]Mock<IChannel<RequestReservationCommand>> channelMock,
            BookingController sut,
            BookingViewModel model)
        {
            sut.Post(model);
            //var expected = model.MakeReservation().AsSource().OfLikeness<RequestReservationCommand>().Without(d => d.Id);
            var expected = new RequestReservationCommandResemblance(model.MakeReservation());
            channelMock.Verify(c => c.Send(It.Is<RequestReservationCommand>(x => expected.Equals(x))));
        }

        private class RequestReservationCommandResemblance : RequestReservationCommand
        {
            public RequestReservationCommandResemblance(RequestReservationCommand source)
                : base(source)
            {
            }

            public override bool Equals(object obj)
            {
                var other = obj as RequestReservationCommand;
                if (other != null)
                    return object.Equals(this.Date, other.Date)
                        && object.Equals(this.Email, other.Email)
                        && object.Equals(this.Name, other.Name)
                        && object.Equals(this.Quantity, other.Quantity);

                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return this.Date.GetHashCode()
                    ^ this.Email.GetHashCode()
                    ^ this.Name.GetHashCode()
                    ^ this.Quantity.GetHashCode();
            }
        }
    }
}
