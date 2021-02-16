using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bestinja.FronEnd.MVC.Controllers;
using Bestinja.Services;
using Bestinja.Services.Contract;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Bestinja.FronEnd.MVC.IoC
{
    public class AppBootsrapper
    {
        public static void WireUp(WindsorContainer container)
        {
            container.Register(Component.For<IUserIdentity>()
                .ImplementedBy<UserIdentity>());
            
            container.Register(Component.For<BaseController>().LifestyleTransient());
            container.Register(Component.For<HomeController>().LifestyleTransient());
            container.Register(Component.For<DropdownController>().LifestyleTransient());
            container.Register(Component.For<ProfilesController>().LifestyleTransient());
            container.Register(Component.For<VendorController>().LifestyleTransient());

        }
    }
}