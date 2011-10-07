using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ploeh.Samples.Booking.WebUI.Windsor
{
    public class WindsorCompositionRoot : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public WindsorCompositionRoot(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)this.container.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }
    }
}