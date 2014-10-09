using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eneco.Invest.Data
{
    public static class InvestDAO
    {
        private static InvestEntities ie = new InvestEntities();

        public static List<Log> FindLastLogMessages(string logType, int maxRecords)
        {
            return ie.Log
                .Where(l => l.Type == logType)
                .OrderByDescending(l => l.CreationDate)
                .Take(maxRecords)
                .ToList();
        }

        public static void WriteLog(string logLevel, string logType, string message)
        {
            ie.Log.Add(new Log { LogLevel = logLevel, Type = logType, Message = message, CreationDate = DateTime.Now });
            ie.SaveChanges();
        }
    }
}
