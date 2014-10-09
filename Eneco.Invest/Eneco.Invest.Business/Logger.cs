using Eneco.Invest.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eneco.Invest.Business
{
    public static class Logger
    {
        private static ILog logger = null;

        public static void Configure(string logName)
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = LogManager.GetLogger(logName);
        }

        public static void Debug(string logType, string message)
        {
            logger.Debug(message);
            InvestDAO.WriteLog("Debug", logType, message);
        }

        public static void Error(string logType, string message, Exception e)
        {
            logger.Error(message, e);
            InvestDAO.WriteLog("Error", logType, message + e.StackTrace);
        }

        public static void DebugFormat(string logType, string messageFormat, params object[] args)
        {
            logger.Debug(string.Format(messageFormat, args));
            InvestDAO.WriteLog("Debug", logType, string.Format(messageFormat, args));
        }
    }
}
