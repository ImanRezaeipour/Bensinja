using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Castle.Windsor;

namespace Bestinja.FronEnd.MVC.IoC
{
    public class CastleControllerActivator: DefaultControllerFactory
    {
        private readonly WindsorContainer _container;

        public CastleControllerActivator(WindsorContainer container)
        {
            _container = container;
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller.GetType());
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return (IHttpController)_container.Resolve(controllerType);
        }
    }
}