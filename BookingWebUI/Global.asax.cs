using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var namespaces = new[] { typeof(HomeController).Namespace };

            routes.MapRoute(
                name: "Booking",
                url: "Booking/{year}.{month}.{day}",
                defaults: new { controller = "Booking", action = "Get" },
                namespaces: namespaces);
            routes.MapRoute(
                name: "DisabledDates",
                url: "DisabledDates",
                defaults: new { controller = "DisabledDates", action = "Get" },
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
        }
    }
}