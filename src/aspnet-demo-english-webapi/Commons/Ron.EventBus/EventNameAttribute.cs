using System;

namespace Ron.EventBus
{
    /// <summary>
    /// 事件队列的事件名字
    /// </summary>
    /// <remarks>
    /// RabbitMQ -> RoutKey
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EventNameAttribute : Attribute
    {
        public EventNameAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; init; }
    }
}
