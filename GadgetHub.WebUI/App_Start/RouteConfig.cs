using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Define routes for different product categories
            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new
                {
                    Controller = "Gadget",
                    action = "List"
                });


            // Define the default route for the application
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Gadget", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
