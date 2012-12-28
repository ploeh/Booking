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
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.Daemon
{
    public class DaemonWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<CommandHandlerConvention>();
            container.AddFacility<EventHandlerConvention>();

            container.Register(Component
                .For<IObserver<object>>()
                .ImplementedBy<CompositeObserver<object>>());

            container.Register(Classes
                .FromAssemblyInDirectory(new AssemblyFilter(".").FilterByName(an => an.Name.StartsWith("Ploeh.Samples.Booking")))
                .Where(Accepted)
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

            GuardAgainstMismatchedCommands(container);
        }

        private static bool Accepted(Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(EventDispatcher<>))
                return false;

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(CommandDispatcher<>))
                return false;

            return true;
        }

        private void GuardAgainstMismatchedCommands(IWindsorContainer container)
        {
            var handlerTypes = from h in container.Kernel.GetHandlers(typeof(IMessage))
                               where h.ComponentModel.Implementation.Name.EndsWith("Command")
                               select typeof(ICommandHandler<>).MakeGenericType(h.ComponentModel.Implementation);
            foreach (var h in handlerTypes)
            {
                var count = container.Kernel.GetHandlers(h).Count();
                if(count != 1)
                    throw new InvalidOperationException(string.Format("Exactly one implementation of {0} was expected, but {1} were found.", h, count));
            }
        }
    }
}
