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
            container.AddFacility<ConsumerConvention>();

            container.Register(Component
                .For<IObserver<object>>()
                .ImplementedBy<CompositeObserver<object>>());

            container.Register(Classes
                .FromAssemblyInDirectory(new AssemblyFilter(".").FilterByName(an => an.Name.StartsWith("Ploeh.Samples.Booking")))
                .Where(t => !(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dispatcher<>)))
                .WithServiceAllInterfaces());

            container.Kernel.Resolver.AddSubResolver(new ExtensionConvention());
            container.Kernel.Resolver.AddSubResolver(new DirectoryConvention(container.Kernel));
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

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
            #endregion
        }
    }
}
