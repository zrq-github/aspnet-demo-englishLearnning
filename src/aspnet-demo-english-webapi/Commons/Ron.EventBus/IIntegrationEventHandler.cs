﻿using System.Threading.Tasks;

namespace Ron.EventBus
{
    public interface IIntegrationEventHandler
    {
        //因为消息可能会重复发送，因此Handle内的实现需要是幂等的
        Task Handle(string eventName, string eventData);
    }
}
