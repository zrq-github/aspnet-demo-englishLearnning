using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConfig.Options
{
    public class JWTOptions
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }

        public string? Key { get; set; }

        public string? ExpireSeconds { get; set; }

        public static JWTOptions Init()
        {
            // todo JWT 系统环境配置
            return new JWTOptions()
            {
                Issuer = "myIssuer",
                Audience ="myAudience",
                Key="qwertyuiop~@#$1234",
                ExpireSeconds="31536000"
            };
        }
    }
}
