using System;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using log4net;

namespace Swaksoft.Application.Seedwork.Logging
{
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        public ILogger Create(Type type)
        {
            var logger = LogManager.GetLogger(type);
            return new Log4NetLogger(logger);
        }

        public ILogger Create<T>()
        {
            return Create(typeof (T));
        }
    }
}
