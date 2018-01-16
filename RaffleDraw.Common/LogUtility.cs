using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace RaffleDraw.Common
{
    public static class LogUtility
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Error(string message, Exception exeption)
        {
            logger.Error(exeption, message);
        }
    }
}
