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
            [Frozen]Mock<IChannel<ReservationAcceptedEvent>> channelMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryStub
                .Setup(r => r.Read(It.IsAny<DateTime>()))
                .Returns(capacity);

            sut.Consume(command);

            var expected = command.Accept().Id;
            channelMock.Verify(c => c.Send(It.Is<ReservationAcceptedEvent>(e => e.Id == expected)));
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

        [Theory, AutoDomainData]
        public void ConsumeWritesEventToRepositoryWhenCapacityIsAvailable(
            [Frozen]Mock<ICapacityRepository> repositoryMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryMock
                .Setup(r => r.Read(command.Date.Date))
                .Returns(capacity);

            var requestWithinCapacity = command.WithQuantity(capacity.Remaining - 1);
            sut.Consume(requestWithinCapacity);

            var expected = requestWithinCapacity.ReserveCapacity().Id;
            repositoryMock.Verify(r => r.Write(It.Is<CapacityReservedEvent>(e => e.Id == expected)));
        }

        [Theory, AutoDomainData]
        public void ConsumeDoesNotWriteEventToRepositoryWhenCapacityIsExceeded(
            [Frozen]Mock<ICapacityRepository> repositoryMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryMock
                .Setup(r => r.Read(command.Date.Date))
                .Returns(capacity);

            sut.Consume(command.WithQuantity(capacity.Remaining + 1));

            repositoryMock.Verify(r => r.Write(It.IsAny<CapacityReservedEvent>()), Times.Never());
        }

        [Theory, AutoDomainData]
        public void ConsumeSendsSoldOutEventWhenSoldOut(
            [Frozen]Mock<ICapacityRepository> repositoryStub,
            [Frozen]Mock<IChannel<SoldOutEvent>> channelMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryStub
                .Setup(r => r.Read(command.Date.Date))
                .Returns(capacity);

            sut.Consume(command.WithQuantity(capacity.Remaining));

            var expected = command.Date.Date;
            channelMock.Verify(c => c.Send(It.Is<SoldOutEvent>(e => e.Date == expected)));
        }

        [Theory, AutoDomainData]
        public void ConsumeDoesNotForwardReplayedEvent(
            [Frozen]Mock<ICapacityRepository> repositoryMock,
            [Frozen]Mock<IChannel<CapacityReservedEvent>> capacityChannelMock,
            [Frozen]Mock<IChannel<SoldOutEvent>> soldOutChannelMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity originalCapacity)
        {
            var requestWithinCapacity = command.WithQuantity(originalCapacity.Remaining - 1);
            var newCapacity = originalCapacity.Reserve(requestWithinCapacity.ReserveCapacity());

            repositoryMock
                .Setup(r => r.Read(command.Date.Date))
                .Returns(newCapacity);

            sut.Consume(requestWithinCapacity);

            repositoryMock.Verify(r => r.Write(It.IsAny<CapacityReservedEvent>()), Times.Never());
            capacityChannelMock.Verify(r => r.Send(It.IsAny<CapacityReservedEvent>()), Times.Never());
            soldOutChannelMock.Verify(r => r.Send(It.IsAny<SoldOutEvent>()), Times.Never());
        }

        [Theory, AutoDomainData]
        public void ConsumeDoesNotSendSoldOutWhenNotSoldOut(
            [Frozen]Mock<ICapacityRepository> repositoryStub,
            [Frozen]Mock<IChannel<SoldOutEvent>> channelMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryStub
                .Setup(r => r.Read(command.Date.Date))
                .Returns(capacity);

            sut.Consume(command.WithQuantity(capacity.Remaining - 1));

            channelMock.Verify(r => r.Send(It.IsAny<SoldOutEvent>()), Times.Never());
        }

        [Theory, AutoDomainData]
        public void ConsumeSendsRejectEventWhenRequestExceedsCapacity(
            [Frozen]Mock<ICapacityRepository> repositoryStub,
            [Frozen]Mock<IChannel<ReservationRejectedEvent>> channelMock,
            CapacityGate sut,
            RequestReservationCommand command,
            Capacity capacity)
        {
            repositoryStub
                .Setup(r => r.Read(command.Date.Date))
                .Returns(capacity);

            var requestExceedingCapacity = command.WithQuantity(capacity.Remaining + 1);
            sut.Consume(requestExceedingCapacity);

            var expected = requestExceedingCapacity.Reject().Id;
            channelMock.Verify(c => c.Send(It.Is<ReservationRejectedEvent>(e => e.Id == expected)));
        }
    }
}
