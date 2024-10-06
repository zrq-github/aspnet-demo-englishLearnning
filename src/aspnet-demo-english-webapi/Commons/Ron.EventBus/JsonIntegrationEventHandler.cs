using System.Text.Json;
using System.Threading.Tasks;

namespace Ron.EventBus
{
    /// <summary>
    /// 集成事件
    /// </summary>
    /// <remarks>
    /// 将接受的消息反序列成原本的对象
    /// </remarks>
    public abstract class JsonIntegrationEventHandler<T> : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string json)
        {
            T? eventData = JsonSerializer.Deserialize<T>(json);
            return HandleJson(eventName, eventData);
        }

        public abstract Task HandleJson(string eventName, T? eventData);
    }
}
