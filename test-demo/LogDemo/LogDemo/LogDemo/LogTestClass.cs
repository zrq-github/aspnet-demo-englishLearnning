using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDemo
{
    internal class LogTestClass(ILogger<LogTestClass> logger)
    {
        public void ConsoleLog()
        {
            logger.LogTrace("log trace...");
            logger.LogDebug("log debug....");
            logger.LogInformation("log information...");
            logger.LogWarning("log warning...");
            logger.LogError("log error...");
        }
    }
}
