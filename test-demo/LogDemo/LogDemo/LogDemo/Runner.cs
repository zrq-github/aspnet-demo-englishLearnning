using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDemo
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }

        public void DoAction(string name)
        {
            _logger.LogDebug(20, "Doing hard work! {Action}", name);
        }

        public void NLogTest()
        {
            _logger.LogTrace("nlog log trace...");
            _logger.LogDebug("nlog log debug...");
            _logger.LogInformation("nlog log info...");
            _logger.LogWarning("nlog log warning...");
            _logger.LogError("nlog log error...");
        }
    }
}
