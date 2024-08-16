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

        public static RedisOptions Init()
        {
            RedisOptions redisOptions = new RedisOptions()
            {
                // docker redis 6379 为默认端口
                ConnStr = "localhost:6379"
            };
            return redisOptions;
        }
    }
}
