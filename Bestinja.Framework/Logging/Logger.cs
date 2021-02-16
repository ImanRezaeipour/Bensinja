using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bestinja.Framework.Logging
{
   public static class Logger
   {
       public static ICusotmLogger Current { get; set; }
       public static void SetCurrent(ICusotmLogger cusotmLogger)
       {
           Current = cusotmLogger;
       }
   }
}
