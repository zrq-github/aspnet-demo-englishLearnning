using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConfig.Options
{
    public class CorsOptions
    {
        public List<string> Origins { get; set; }

        public static CorsOptions InitCorsOptions()
        {
            CorsOptions options = new CorsOptions()
            {
                Origins = new List<string> { "http://localhost:3000", "http://localhost:3001" }
            };
            return options;
        }
    }
}
