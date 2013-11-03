using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net.Http.Formatting;

namespace api_electronia_articulos
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            ConfigureApi(GlobalConfiguration.Configuration);
            
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           // var config = GlobalConfiguration.Configuration;
           // config.Formatters.Insert(0, new JsonpMediaTypeFormatter());
        }

        void ConfigureApi(HttpConfiguration config)
        {
            // Remove the JSON formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // or

            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
           
        }
    }



   
}