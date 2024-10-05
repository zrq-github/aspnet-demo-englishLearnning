using Ron.EventBus;

namespace CommonInitializer
{
    public class InitializerOptions: IEventBusQueueName
    {
        public string LogFilePath { get; set; }

        /// <summary>
        /// 集成事件队列名字
        /// </summary>
        /// <remarks>
        /// 用于EventBus的QueueName，因此要维持“同一个项目值保持一直，不同项目不能冲突”
        /// </remarks>
        public string EventBusQueueName { get; set; }
    }
}
