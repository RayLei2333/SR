using NLog;
using System;

namespace SR.Infrastructure
{
    public static class LogService
    {
        private static Logger _logger = LogManager.GetLogger("SR");
        public static void Trace(string msg)
        {
            _logger.Trace(msg);
        }

        public static void Trace(Exception exception)
        {
            _logger.Trace(exception);
        }

        public static void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public static void Debug(Exception exception)
        {
            _logger.Debug(exception);
        }

        public static void Error(string msg)
        {
            _logger.Error(msg);
        }

        public static void Error(Exception exception)
        {
            _logger.Error(exception);
        }
    }
}
