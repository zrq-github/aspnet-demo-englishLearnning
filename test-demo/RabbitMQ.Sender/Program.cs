using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ.Sender;

internal class Program
{
    private static async Task Sender()
    {
        ConnectionFactory factory = new ConnectionFactory();
        // "guest"/"guest" by default, limited to localhost connections
        factory.UserName = ConnectionFactory.DefaultUser;
        factory.Password = ConnectionFactory.DefaultPass;
        factory.VirtualHost = ConnectionFactory.DefaultVHost;
        factory.HostName = "localhost";

        using var conn = factory.CreateConnection();
        using var channel = conn.CreateModel();
        channel.ExchangeDeclare("direct_logs", ExchangeType.Direct);
        channel.QueueDeclare("queue_logs", false, false, false, null);
        channel.QueueBind("queue_logs", "direct_logs", "logs");

        // Passive Declaration - 被动宣言
        // 如果队列不存在，会抛出错误
        var response = channel.QueueDeclarePassive("queue_logs");
        var messageCount = response.MessageCount;   // returns the number of messages in Ready state in the queue
        var consumerCount = response.ConsumerCount; // returns the number of consumers the queue has

        // 清除队列中的消息
        var queuePurge = channel.QueuePurge("queue_logs");

        // 创建消息属性
        IBasicProperties props = channel.CreateBasicProperties();
        props.ContentType = "text/plain";
        props.DeliveryMode = 2;        // 设置为持久化模式
        props.Expiration = "36000000"; // 设置消息过期时间为 10 小时

        // 循环发送消息
        while (true)
        {
            response = channel.QueueDeclarePassive("queue_logs");
            Console.WriteLine($"messageCount:{response.MessageCount}, consumerCount:{response.ConsumerCount}");
            string message = $"sender send message: dateTime {DateTime.Now}";
            channel.BasicPublish("direct_logs", "logs", props, Encoding.UTF8.GetBytes(message));
            Console.WriteLine();
            await Task.Delay(1000);
        }

        channel.Close();
        conn.Close();
    }

    private static async Task Main(string[] args)
    {
        await Sender();

        Console.ReadKey();
    }
}
