using System;

namespace Ron.EventBus;

/// <summary>
/// 集成事件接口
/// </summary>
/// <remarks>
/// 是<see cref="RabbitMQEventBus"/>的接口
/// 提供简化RabbitMQ的使用
/// </remarks>
public interface IEventBus
{
    /// <summary>
    /// 推送
    /// </summary>
    /// <param name="eventName">事件名字</param>
    /// <param name="eventData">事件数据</param>
    void Publish(string eventName, object? eventData);

    void Subscribe(string eventName, Type handlerType);

    void Unsubscribe(string eventName, Type handlerType);
}
