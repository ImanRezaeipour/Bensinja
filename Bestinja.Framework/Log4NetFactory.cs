using log4net;

namespace Bestinja.Framework
{
    public  class Log4NetFactory
    {
        public  ILog Create()
        {
            return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        }

    }
}