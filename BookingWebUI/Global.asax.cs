using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ploeh.Samples.Booking.WebModel;
using Castle.Windsor;
using Ploeh.Samples.Booking.WebUI.Windsor;

namespace Ploeh.Samples.Booking.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container;

        public MvcApplication()
        {
            this.container = new WindsorContainer().Install(new WebWindsorInstaller());
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var namespaces = new[] { typeof(HomeController).Namespace };

            routes.MapRoute(
                name: "Post",
                url: "{Controller}/{id}",
                defaults: new { controller = "Home", action = "Post" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") },
                namespaces: namespaces);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{id}",
                defaults: new { controller = "Home", action = "Get", id = UrlParameter.Optional },
                namespaces: namespaces);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new WindsorCompositionRoot(this.container));
        }

        public override void Dispose()
        {
            this.container.Dispose();
            base.Dispose();
        }
    }
}