using System;

namespace ConfigServices
{
    public interface IConfigServices
    {
        public string GetValue(string key);
    }
}
