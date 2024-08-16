using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConfig.Options
{
    public class ElasticSearchOptions
    {
        public System.Uri?  Uri { get; set; }

        public static ElasticSearchOptions Init()
        {
            // todo ElasticSearchOptions 需要搭建环境后再配置
            return new ElasticSearchOptions()
            {
               Uri = null,
            };
        }
    }
}
