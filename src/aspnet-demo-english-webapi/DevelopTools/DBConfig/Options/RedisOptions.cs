using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConfig.Options
{
    public class RedisOptions
    {
        public string ConnStr { get; set; }

        public static RedisOptions InitOptions()
        {
            RedisOptions redisOptions = new RedisOptions()
            {
                ConnStr = "localhost"
            };
            return redisOptions;
        }
    }
}
