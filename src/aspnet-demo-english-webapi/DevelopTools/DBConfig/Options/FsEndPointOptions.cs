using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConfig.Options
{
    public class FsEndPointOptions
    {
        public System.Uri? UrlRoot { get;private set; }
        public static FsEndPointOptions Init()
        {
            return new FsEndPointOptions()
            {
                UrlRoot = new Uri("http://localhost/FileService"),
            };
        }
    }
}
