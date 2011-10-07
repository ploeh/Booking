using System;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.Samples.Booking.JsonAntiCorruption;
using Ploeh.Samples.Booking.Persistence.FileSystem;
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.WebUI
{
    public class PoorMansCompositionRoot : DefaultControllerFactory
    {
        private readonly DirectoryInfo queueDirectory;
        private readonly DirectoryInfo singleSourceOfTruthDirectory;
        private readonly DirectoryInfo viewStoreDirectory;

        public PoorMansCompositionRoot()
        {
            this.queueDirectory = 
                new DirectoryInfo(HostingEnvironment.MapPath("~/Queue")).CreateIfAbsent();
            this.singleSourceOfTruthDirectory = 
                new DirectoryInfo(HostingEnvironment.MapPath("~/SSoT")).CreateIfAbsent();
            this.viewStoreDirectory = 
                new DirectoryInfo(HostingEnvironment.MapPath("~/ViewStore")).CreateIfAbsent();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            var extension = "txt";

            if (controllerType == typeof(HomeController))
            {
                return new HomeController(
                    new FileMonthViewStore(
                        this.viewStoreDirectory,
                        extension));
            }
            if (controllerType == typeof(DisabledDatesController))
            {
                return new DisabledDatesController(
                    new FileMonthViewStore(
                        this.viewStoreDirectory,
                        extension));
            }
            if (controllerType == typeof(BookingController))
            {
                var fileDateStore = new FileDateStore(
                    singleSourceOfTruthDirectory,
                    extension);

                var quickenings = new IQuickening[]
                {
                    new RequestReservationCommand.Quickening(),
                    new ReservationAcceptedEvent.Quickening(),
                    new ReservationRejectedEvent.Quickening(),
                    new CapacityReservedEvent.Quickening(),
                    new SoldOutEvent.Quickening()
                };

                return new BookingController(
                    new JsonCapacityRepository(
                        fileDateStore,
                        fileDateStore,
                        quickenings),
                    new JsonChannel<RequestReservationCommand>(
                        new FileQueueWriter<RequestReservationCommand>(
                            this.queueDirectory,
                            extension)));
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}