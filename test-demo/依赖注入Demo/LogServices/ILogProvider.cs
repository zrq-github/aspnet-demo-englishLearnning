using System;

namespace LogServices
{
    public interface ILogProvider
    {
        public void LogError(in string msg);

        public void LogWarning(in string msg);

        public void LogInfo(in string msg);
    }
}
