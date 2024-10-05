namespace Ron.EventBus;

/// <summary>
/// 集成事件队列名字
/// </summary>
/// <remarks>
/// <see cref="RabbitMQ.Client.IModel.QueueBind(string, string, string, System.Collections.Generic.IDictionary{string, object})"/>
/// </remarks>
public interface IEventBusQueueName
{
    public string EventBusQueueName { get; set; }
}
