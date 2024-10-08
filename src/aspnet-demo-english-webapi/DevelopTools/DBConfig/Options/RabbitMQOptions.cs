using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConfig.Options
{
    public class RabbitMQOptions
    {
        public string HostName { get;set; }
        public string ExchangeName { get; set; }

        public static RabbitMQOptions Init()
        {
            RabbitMQOptions rabbitMQOptions = new()
            {
                HostName = "127.0.0.1",
                ExchangeName = "zrq_event_bus"
            };
            return rabbitMQOptions;
        }
    }
}
