using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;
using Xunit.Extensions;
using Xunit;
using Ploeh.AutoFixture.Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class ReservertionRejectedEventTests
    {
        [Theory, AutoDomainData]
        public void SutIsMessage(ReservationRejectedEvent sut)
        {
            Assert.IsAssignableFrom<IMessage>(sut);
        }

        [Theory, AutoDomainData]
        public void IdIsCorrect([Frozen]Guid id, ReservationRejectedEvent sut)
        {
            Assert.Equal<Guid>(id, sut.Id);
        }

        [Theory, AutoDomainData]
        public void DateIsCorrect([Frozen]DateTime date, ReservationRejectedEvent sut)
        {
            Assert.Equal<DateTime>(date, sut.Date);
        }

        [Theory, AutoDomainData]
        public void NameIsCorrect([Frozen]string name, ReservationRejectedEvent sut)
        {
            Assert.Equal<string>(name, sut.Name);
        }

        [Theory, AutoDomainData]
        public void EmailIsCorrect([Frozen]string email, ReservationRejectedEvent sut)
        {
            Assert.Equal<string>(email, sut.Email);
        }
    }
}
