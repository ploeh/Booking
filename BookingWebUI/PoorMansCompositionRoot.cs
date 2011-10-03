using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.WebUI
{
    public class PoorMansCompositionRoot : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(BookingController))
            {
                return new BookingController(
                    new FixedRemainingCapacityReader());
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
    }
}