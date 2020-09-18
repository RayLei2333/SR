using NLog;
using System;

namespace SR.Infrastructure
{
    public static class LogService
    {
        //实例日志对象
        private static Logger _logger = LogManager.GetLogger("SR");
        //追踪日志
        public static void Trace(string msg)
        {
            _logger.Trace(msg);
        }

        public static void Trace(Exception exception)
        {
            _logger.Trace(exception);
        }
        //调试日志
        public static void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public static void Debug(Exception exception)
        {
            _logger.Debug(exception);
        }
        //错误日志
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
