using System;
using Microsoft.AspNetCore.Builder;

namespace Ron.EventBus;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 使用事件总线
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// 获得IEventBus一次，就会立即加载IEventBus，这样扫描所有的EventHandler，保证消息及时消费
    /// </remarks>
    public static IApplicationBuilder UseEventBus(this IApplicationBuilder appBuilder)
    {
        //获得IEventBus一次，就会立即加载IEventBus，这样扫描所有的EventHandler，保证消息及时消费
        var eventBus = appBuilder.ApplicationServices.GetService(typeof(IEventBus));
        //if (eventBus == null)
        //    throw new ApplicationException("找不到IEventBus实例");

        return appBuilder;
    }
}
