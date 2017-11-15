using System;
using NLog;

namespace BookLibrary.Loggers.Adapters
{
    public class NLogAdapter : ILogger
    {
        private readonly Logger logger;

        public NLogAdapter(string loggerName)
        {
            logger = LogManager.GetLogger(loggerName);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(Exception e)
        {
            logger.Error(e);
        }

        public void Fatal(string message)
        {
            logger.Error(message);
        }
    }
}
