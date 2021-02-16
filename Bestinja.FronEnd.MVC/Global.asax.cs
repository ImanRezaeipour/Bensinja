using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bestinja.Framework;
using Bestinja.FronEnd.MVC.IoC;
using Castle.Windsor;
using WebGrease.Configuration;

namespace Bestinja.FronEnd.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.All;

            var container = new WindsorContainer();
            Bootsrapper.WireUp(container);
            AppBootsrapper.WireUp(container);
            var controllerFactory = new WindsorControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

             

        }
    }
}
