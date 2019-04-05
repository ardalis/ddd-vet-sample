using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace FrontDesk.Web.AppStart
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Rooms",
                url: "Rooms",
                defaults: new { controller = "Home", action = "Rooms" }
            );
            routes.MapRoute(
                name: "Clients",
                url: "Clients",
                defaults: new { controller = "Home", action = "Clients" }
            );
            routes.MapRoute(
                name: "Doctors",
                url: "Doctors",
                defaults: new { controller = "Home", action = "Doctors" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}