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
            return new ElasticSearchOptions()
            {
               Uri = new Uri("http://localhost:9200/"),
            };
        }
    }
}
