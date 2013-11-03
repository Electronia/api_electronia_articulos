using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace api_electronia_articulos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            routes.MapHttpRoute(
               name: "DefaultApiItem",
               routeTemplate: "{controller}/{userId}",
               defaults: new { id = RouteParameter.Optional }
            );

            routes.MapHttpRoute(
               name: "DefaultApiCategory",
               routeTemplate: "{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );
           
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action="Index", id = UrlParameter.Optional }
            );
        }
    }
}