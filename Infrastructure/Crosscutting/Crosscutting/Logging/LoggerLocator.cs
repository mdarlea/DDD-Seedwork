using System;

namespace Swaksoft.Infrastructure.Crosscutting.Logging
{
    /// <summary>
    /// Log Factory
    /// </summary>
    public static class LoggerLocator
    {
        static ILoggerFactory _currentLogFactory = null;
        private static readonly object _thisObject = new object();
        
        /// <summary>
        /// Set the  log factory to use
        /// </summary>
        /// <param name="logFactory">Log factory to use</param>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            lock (_thisObject)
            {
                _currentLogFactory = logFactory;
            }
        }

        /// <summary>
        /// Createt a new logger
        /// </summary>
        /// <returns>Created ILog</returns>
        public static ILogger CreateLog(Type type)
        {
            return (_currentLogFactory != null) ? _currentLogFactory.Create(type) : null;
        }

        public static ILogger CreateLog<T>()
        {
            return (_currentLogFactory != null) ? _currentLogFactory.Create<T>() : null;
        }
    }
}
