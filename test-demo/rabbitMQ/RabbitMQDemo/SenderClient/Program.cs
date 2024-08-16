using RabbitMQ.Client;
using System.Text;

namespace SenderClient;

internal class Program
{
    private static void Main(string[] args)
    {
        // 配置连接工厂
        var factory = new ConnectionFactory()
        {
            HostName = "localhost", // RabbitMQ 容器所在的主机名（或 IP 地址）
            Port = 5672,            // RabbitMQ 的端口
            UserName = "guest",     // 默认用户名
            Password = "guest"      // 默认密码
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var queueName = "queue1";

        var exchangeName = "exchange1";
        var eventName = "myEvent";
        
        // 声明队列
        channel.QueueDeclare(queueName,
            true,
            false,
            false,
            null);

        while (true)
        {
            var msg = DateTime.Now.TimeOfDay.ToString();
            var body = Encoding.UTF8.GetBytes(msg);

            channel.BasicPublish("", queueName, null, body);

            Console.WriteLine("SenderClient Send msg:" + msg);
            Thread.Sleep(1000);
        }
    }
}
