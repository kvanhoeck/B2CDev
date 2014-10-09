using Eneco.Invest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Eneco.Invest.Business
{
    public class InvestBusiness
    {
        public InvestBusiness()
        {
            Logger.Configure("LogFileAppender");
        }

        public List<Log> FindLastLogMessages(string logType, int maxRecords)
        {
            return InvestDAO.FindLastLogMessages(logType, maxRecords);
        }

        public void UploadIsabelFile(byte[] file)
        {
            Logger.DebugFormat("Isabel", "Start parsing file of length {0}", file.Length);
            Thread.Sleep(1000);
        }

        public void SaveIsabelFile()
        {
            Logger.DebugFormat("Isabel", "Start saving file");
            Thread.Sleep(1000);
        }

        public void MatchIsabelFile()
        {
            Logger.DebugFormat("Isabel", "Start matching...");
            Thread.Sleep(1000);
        }
    }
}
