using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Ploeh.Samples.Booking.PersistenceModel;
using Ploeh.Samples.Booking.Persistence.FileSystem;
using System.IO;
using Ploeh.Samples.Booking.JsonAntiCorruption;
using Ploeh.Samples.Booking.DomainModel;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using System.Reactive.Subjects;
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.Daemon
{
    public class DaemonWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            #region Manual configuration that requires maintenance
            container.Register(Component
                .For<DirectoryInfo>()
                .UsingFactoryMethod(() =>
                    new DirectoryInfo(@"..\..\..\BookingWebUI\Queue").CreateIfAbsent())
                .Named("queueDirectory"));
            container.Register(Component
                .For<DirectoryInfo>()
                .UsingFactoryMethod(() =>
                    new DirectoryInfo(@"..\..\..\BookingWebUI\SSoT").CreateIfAbsent())
                .Named("ssotDirectory"));
            container.Register(Component
                .For<DirectoryInfo>()
                .UsingFactoryMethod(() =>
                    new DirectoryInfo(@"..\..\..\BookingWebUI\ViewStore").CreateIfAbsent())
                .Named("viewStoreDirectory"));            

            container.Register(Component
                .For<IQueue>()
                .ImplementedBy<FileQueue>()
                .DependsOn(
                    Dependency.OnComponent("directory", "queueDirectory"),
                    Dependency.OnValue("extension", "txt")));

            container.Register(Component
                .For<IStoreWriter<DateTime>, IStoreReader<DateTime>>()
                .ImplementedBy<FileDateStore>()
                .DependsOn(
                    Dependency.OnComponent("directory", "ssotDirectory"),
                    Dependency.OnValue("extension", "txt")));
            container.Register(Component
                .For<IStoreWriter<ReservationAcceptedEvent>>()
                .ImplementedBy<FileQueueWriter<ReservationAcceptedEvent>>()
                .DependsOn(
                    Dependency.OnComponent("directory", "queueDirectory"),
                    Dependency.OnValue("extension", "txt")));
            container.Register(Component
                .For<IStoreWriter<ReservationRejectedEvent>>()
                .ImplementedBy<FileQueueWriter<ReservationRejectedEvent>>()
                .DependsOn(
                    Dependency.OnComponent("directory", "queueDirectory"),
                    Dependency.OnValue("extension", "txt")));
            container.Register(Component
                .For<IStoreWriter<SoldOutEvent>>()
                .ImplementedBy<FileQueueWriter<SoldOutEvent>>()
                .DependsOn(
                    Dependency.OnComponent("directory", "queueDirectory"),
                    Dependency.OnValue("extension", "txt")));

            container.Register(Component
                .For<IChannel<ReservationAcceptedEvent>>()
                .ImplementedBy<JsonChannel<ReservationAcceptedEvent>>());
            container.Register(Component
                .For<IChannel<ReservationRejectedEvent>>()
                .ImplementedBy<JsonChannel<ReservationRejectedEvent>>());
            container.Register(Component
                .For<IChannel<SoldOutEvent>>()
                .ImplementedBy<JsonChannel<SoldOutEvent>>());

            container.Register(Component
                .For<ICapacityRepository>()
                .ImplementedBy<JsonCapacityRepository>());

            container.Register(Component
                .For<IConsumer<RequestReservationCommand>>()
                .ImplementedBy<CapacityGate>());
            container.Register(Component
                .For<IConsumer<SoldOutEvent>>()
                .ImplementedBy<MonthViewUpdater>());

            container.Register(Component
                .For<Dispatcher<RequestReservationCommand>>());
            container.Register(Component
                .For<Dispatcher<SoldOutEvent>>());

            container.Register(Component
                .For<IObserver<Stream>>()
                .ImplementedBy<JsonStreamObserver>());
            container.Register(Component
                .For<IObserver<DateTime>>()
                .ImplementedBy<FileMonthViewStore>()
                .DependsOn(
                    Dependency.OnComponent("directory", "viewStoreDirectory"),
                    Dependency.OnValue("extension", "txt")));
            container.Register(Component
                .For<IObserver<object>>()
                .UsingFactoryMethod(k =>
                {
                    var messageDispatcher = new Subject<object>();
                    messageDispatcher.Subscribe(k.Resolve<Dispatcher<RequestReservationCommand>>());
                    messageDispatcher.Subscribe(k.Resolve<Dispatcher<SoldOutEvent>>());
                    return messageDispatcher;
                }));

            container.Register(Component
                .For<IQuickening>()
                .ImplementedBy<RequestReservationCommand.Quickening>());
            container.Register(Component
                .For<IQuickening>()
                .ImplementedBy<ReservationAcceptedEvent.Quickening>());
            container.Register(Component
                .For<IQuickening>()
                .ImplementedBy<ReservationRejectedEvent.Quickening>());
            container.Register(Component
                .For<IQuickening>()
                .ImplementedBy<CapacityReservedEvent.Quickening>());
            container.Register(Component
                .For<IQuickening>()
                .ImplementedBy<SoldOutEvent.Quickening>());

            container.Register(Component
                .For<QueueConsumer>());

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            #endregion
        }
    }
}
