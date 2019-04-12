using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Platform
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "Payment-New",
               url: "Payment/new",
               defaults: new { controller = "Payment", action = "Index" },
               constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "GET" }) }
           );

            routes.MapRoute(
               name: "Payment",
               url: "Payment",
               defaults: new { controller = "Payment", action = "Index" },
               constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "GET" }) }
           );

            routes.MapRoute(
                name: "Payment-Create",
                url: "Payment",
                defaults: new { controller = "Payment", action = "Create" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "POST" }) }
            );

            routes.MapRoute(
                name: "Payment-Subscription",
                url: "Payment",
                defaults: new { controller = "Payment", action = "Subscription" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "POST" }) }
            );

            routes.MapRoute(
                name: "Payment-Show",
                url: "Payment/{id}",
                defaults: new { controller = "Payment", action = "Show" }
            );

            //routes.MapRoute(
            //    name: "Home",
            //    url: "",
            //    defaults: new { controller = "Checkouts", action = "New" }
            //);

        }
    }
}
