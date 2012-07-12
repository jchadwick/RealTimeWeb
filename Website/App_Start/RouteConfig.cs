using System.Web.Mvc;
using System.Web.Routing;

namespace RealTimeWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Demos",
                url: "Demos/{name}",
                defaults: new { controller = "Demos", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Demos", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}