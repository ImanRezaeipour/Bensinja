using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bestinja.Framework.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using log4net;

namespace Bestinja.Framework
{
   public class Bootsrapper
    {
        public static void WireUp(WindsorContainer container)
        {
            //var adapter = new Log4Net(new Log4NetFactory().Create());
         
                    var adapter = new Log4Net(new Log4NetFactory().Create());
            container.Register(Component.For<ICusotmLogger>().Instance(adapter).LifestyleSingleton());
            Logger.SetCurrent(adapter);
        } 
    }
}
