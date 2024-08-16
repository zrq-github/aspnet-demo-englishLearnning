using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ExpenseClient;

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

        // 创建消费者对象
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) => {

            byte[] message = ea.Body.ToArray();
            Console.WriteLine("接收到的消息为：" + Encoding.UTF8.GetString(message));
        };

        // 消费者开启监听
        channel.BasicConsume(queueName, true, consumer);

        Console.ReadKey();
        channel.Dispose();
        connection.Close();
    }
}
