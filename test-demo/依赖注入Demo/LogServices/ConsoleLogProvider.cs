using System;

namespace LogServices
{
    /// <summary>
    /// 控制台Log输入
    /// </summary>
    public class ConsoleLogProvider : ILogProvider
    {
        #region ILogProvider Members

        public void LogError(in string msg)
        {
            Console.WriteLine($"Error:{msg}");
        }

        public void LogWarning(in string msg) 
        {
            Console.WriteLine($"Warn:{msg}"); 
        }

        public void LogInfo(in string msg)
        {
            Console.WriteLine($"Info:{msg}"); 
        }

        #endregion
    }
}
