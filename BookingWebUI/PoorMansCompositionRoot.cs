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

        public PoorMansCompositionRoot()
        {
            var queuePath = HostingEnvironment.MapPath("~/Queue");
            this.queueDirectory = new DirectoryInfo(queuePath);
            if (!this.queueDirectory.Exists)
                this.queueDirectory.Create();

            var ssotPath = HostingEnvironment.MapPath("~/SSoT");
            this.singleSourceOfTruthDirectory = new DirectoryInfo(ssotPath);
            if (!this.singleSourceOfTruthDirectory.Exists)
                this.singleSourceOfTruthDirectory.Create();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(BookingController))
            {
                var extension = "txt";

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