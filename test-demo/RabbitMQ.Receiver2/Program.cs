using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Receiver;

internal class Program
{
    private static Task Receiver()
    {
        return Task.CompletedTask;
    }

    private static async Task Main(string[] args)
    {
        var factory = new ConnectionFactory();
        // "guest"/"guest" by default, limited to localhost connections
        factory.UserName = ConnectionFactory.DefaultUser;
        factory.Password = ConnectionFactory.DefaultPass;
        factory.VirtualHost = ConnectionFactory.DefaultVHost;
        factory.HostName = "localhost";

        factory.DispatchConsumersAsync = true;

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

        // 接受消息
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Receiver2 accept message: {message}");
            // copy or deserialise the payload
            // and process the message
            // ...

            //channel?.BasicAck(ea.DeliveryTag, false);
            return Task.CompletedTask;
        };
        // this consumer tag identifies the subscription
        // when it has to be cancelled
        var consumerTag = channel.BasicConsume("queue_logs", true, consumer);
        // ensure we get a delivery
        //bool waitRes = latch.WaitOne(2000);

        Console.WriteLine("按回车退出");
        Console.ReadLine();

        channel.Close();
        conn.Close();
    }
}
