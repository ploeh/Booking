using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Ploeh.Samples.Booking.DomainModel;
using Xunit;
using Ploeh.AutoFixture.Xunit;
using Moq;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class CapacityGateTests
    {
        [Theory, AutoDomainData]
        public void SutIsConsumer(CapacityGate sut)
        {
            Assert.IsAssignableFrom<IConsumer<RequestReservationCommand>>(sut);
        }

        [Theory, AutoDomainData]
        public void ConsumeRequestWithEnoughCapacitySendsCorrectEvent(
            [Frozen]Mock<ICapacityRepository> repositoryStub,
            [Frozen]Mock<IChannel<CapacityReservedEvent>> channelMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryStub
                .Setup(r => r.Read(It.IsAny<DateTime>()))
                .Returns(capacity);

            sut.Consume(command);

            var expected = command.ReserveCapacity().Id;
            channelMock.Verify(c => c.Send(It.Is<CapacityReservedEvent>(e => e.Id == expected)));
        }

        [Theory, AutoDomainData]
        public void ConsumeDoesNotSendReservedEventWhenCapacityIsExceeded(
            [Frozen]Mock<ICapacityRepository> repositoryStub,
            [Frozen]Mock<IChannel<CapacityReservedEvent>> channelMock,            
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryStub
                .Setup(r => r.Read(command.Date.Date))
                .Returns(capacity);

            sut.Consume(command.WithQuantity(capacity.Remaining + 1));

            channelMock.Verify(c => c.Send(It.IsAny<CapacityReservedEvent>()), Times.Never());
        }
    }
}
