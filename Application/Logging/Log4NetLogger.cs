using System;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using log4net;

namespace Swaksoft.Application.Seedwork.Logging
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(ILog log)
        {
            if (log == null) throw new ArgumentNullException("log");
            _log = log;
        }

        public void Debug(string message, params object[] args)
        {
            _log.DebugFormat(message,args);
        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            _log.DebugFormat(string.Format(message, args),exception);
        }

        public void Debug(object item)
        {
            if (item == null) throw new ArgumentNullException("item");
            _log.Debug(item.ToString());
        }

        public void Fatal(string message, params object[] args)
        {
            _log.FatalFormat(message, args);
        }
         
        public void Fatal(string message, Exception exception, params object[] args)
        {
            _log.Fatal(string.Format(message, args),exception);
        }

        public void LogInfo(string message, params object[] args)
        {
            _log.InfoFormat(message,args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _log.WarnFormat(message,args);
        }

        public void LogError(string message, params object[] args)
        {
            _log.ErrorFormat(message,args);
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            _log.Error(string.Format(message,args),exception);
        }
    }
}
