using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ploeh.Samples.Booking.WebModel;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.Samples.Booking.JsonAntiCorruption;
using Ploeh.Samples.Booking.Persistence.FileSystem;
using System.IO;
using System.Web.Hosting;

namespace Ploeh.Samples.Booking.WebUI
{
    public class PoorMansCompositionRoot : DefaultControllerFactory
    {
        private readonly DirectoryInfo queueDirectory;

        public PoorMansCompositionRoot()
        {
            var queuePath = HostingEnvironment.MapPath("~/queue");
            if (!Directory.Exists(queuePath))
                Directory.CreateDirectory(queuePath);
            this.queueDirectory = new DirectoryInfo(queuePath);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(BookingController))
            {
                return new BookingController(
                    new FixedRemainingCapacityReader(),
                    new JsonChannel<MakeReservationCommand>(
                        new FileQueueWriter<MakeReservationCommand>(
                            this.queueDirectory)));
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }

        private class FixedRemainingCapacityReader : IReader<DateTime, int>
        {
            public int Query(DateTime arg)
            {
                return 10;
            }
        }

        private class NullChannel<T> : IChannel<T> where T : IMessage
        {
            public void Send(T message)
            {
            }
        }
    }
}