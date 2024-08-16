using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConfig.Options;

public class SMBStorageOptions
{
    public string WorkingDir { get; set; } = string.Empty;

    public SMBStorageOptions Init()
    {
        return new SMBStorageOptions()
        {
            WorkingDir = Path.Combine(Path.GetTempPath(), "aspnet-demo", "SMBStorage")
        };
    }   
}
