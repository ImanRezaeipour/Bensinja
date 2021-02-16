using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bestinja.Framework.Logging;
using log4net;
using log4net.Config;

namespace Bestinja.Framework
{
    public class Log4Net : ICusotmLogger
    {
        private readonly ILog _log;

        public Log4Net(ILog log)
        {
            _log = log;
        }
        public void Error(Exception ex)
        {
            if (this._log.IsErrorEnabled)
            {
                _log.Error(ex.Message);
            }
        }

        public void Debug(string message, Exception ex)
        {
            _log.Debug(message, ex);
        }

        public void Warn(string message, Exception ex)
        {
            _log.Warn(message, ex);
        }
    }
}
