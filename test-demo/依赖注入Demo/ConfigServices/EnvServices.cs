using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigServices
{
    public class EnvServices:IConfigServices
    {
        public string GetValue(string key)
        {
            return $"EvbServices:{key}";
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
