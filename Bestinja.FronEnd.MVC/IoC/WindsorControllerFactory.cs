using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Bestinja.Framework;
using Bestinja.Framework.Logging;
using Bestinja.FronEnd.MVC.Controllers;
using Bestinja.Services;
using Bestinja.Services.Contract;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Bestinja.FronEnd.MVC.IoC
{

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public override void ReleaseController(IController controller)
        {
            container.Release(controller.GetType());
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
       

            if (controllerType == null)
            {
                IEnumerable<Type> controllerTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where typeof(IController).IsAssignableFrom(t)
                    select t;
                foreach (Type t in controllerTypes)
                {
                    return base.GetControllerInstance(requestContext, t);
                }

            }
            return (IController)container.Resolve(controllerType);
        }
    }

}