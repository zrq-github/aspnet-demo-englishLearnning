using System.Threading.Tasks;
using Dynamic.Json;

namespace Ron.EventBus;

/// <summary>
/// 集成事件
/// </summary>
/// <remarks>
/// 将接受的消息反序列成动态类型
/// </remarks>
public abstract class DynamicIntegrationEventHandler : IIntegrationEventHandler
{
    #region IIntegrationEventHandler Members

    public Task Handle(string eventName, string eventData)
    {
        #region dotnet6一些问题

        //https://github.com/dotnet/runtime/issues/53195
        //https://github.com/dotnet/core/issues/6444
        //.NET 6目前不支持把json反序列化为dynamic，本来preview 4支持，但是在preview 7又去掉了

        #endregion

        //所以暂时用Dynamic.Json来实现。
        var dynamicEventData = DJson.Parse(eventData);
        return HandleDynamic(eventName, dynamicEventData);
    }

    #endregion

    public abstract Task HandleDynamic(string eventName, dynamic eventData);
}
