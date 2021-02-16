using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bestinja.Framework.Logging
{
    public interface ICusotmLogger
    {
        void Error(Exception ex);
        void Debug(string message ,Exception ex);
        void Warn(string message ,Exception ex);
    }
}
