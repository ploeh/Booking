using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Ploeh.Samples.Booking.WebModel;
using System.Web.Mvc;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.Samples.Booking.Persistence.FileSystem;
using System.IO;
using System.Web.Hosting;
using Ploeh.Samples.Booking.JsonAntiCorruption;
using Ploeh.Samples.Booking.PersistenceModel;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

namespace Ploeh.Samples.Booking.WebUI.Windsor
{
    public class WebWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                .FromAssemblyContaining<HomeController>()
                .BasedOn<IController>()
                .LifestylePerWebRequest());

            container.Register(Classes
                .FromAssemblyInDirectory(new AssemblyFilter("bin").FilterByName(an => an.Name.StartsWith("Ploeh.Samples.Booking")))
                .BasedOn<IQuickening>()
                .WithService.FromInterface());

            #region Manual configuration that requires maintenance
            container.Register(Component
                .For<DirectoryInfo>()
                .UsingFactoryMethod(() =>
                    new DirectoryInfo(HostingEnvironment.MapPath("~/Queue")).CreateIfAbsent())
                .Named("queueDirectory"));
            container.Register(Component
                .For<DirectoryInfo>()
                .UsingFactoryMethod(() =>
                    new DirectoryInfo(HostingEnvironment.MapPath("~/SSoT")).CreateIfAbsent())
                .Named("ssotDirectory"));
            container.Register(Component
                .For<DirectoryInfo>()
                .UsingFactoryMethod(() =>
                    new DirectoryInfo(HostingEnvironment.MapPath("~/ViewStore")).CreateIfAbsent())
                .Named("viewStoreDirectory"));

            container.Register(Component
                .For<IReader<Month, IEnumerable<string>>>()
                .ImplementedBy<FileMonthViewStore>()
                .DependsOn(
                    Dependency.OnComponent("directory", "viewStoreDirectory"),
                    Dependency.OnValue("extension", "txt")));
            container.Register(Component
                .For<IReader<DateTime, int>>()
                .ImplementedBy<JsonCapacityRepository>());

            container.Register(Component
                .For<IStoreWriter<DateTime>, IStoreReader<DateTime>>()
                .ImplementedBy<FileDateStore>()
                .DependsOn(
                    Dependency.OnComponent("directory", "ssotDirectory"),
                    Dependency.OnValue("extension", "txt")));
            container.Register(Component
                .For<IStoreWriter<RequestReservationCommand>>()
                .ImplementedBy<FileQueueWriter<RequestReservationCommand>>()
                .DependsOn(
                    Dependency.OnComponent("directory", "queueDirectory"),
                    Dependency.OnValue("extension", "txt")));

            container.Register(Component
                .For<IChannel<RequestReservationCommand>>()
                .ImplementedBy<JsonChannel<RequestReservationCommand>>());

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            #endregion
        }
    }
}