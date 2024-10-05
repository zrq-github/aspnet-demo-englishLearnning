using MediatR;
using System.Collections.Generic;

namespace Ron.DomainCommons.Models
{
    /// <summary>
    /// 领域事件，聚合根
    /// </summary>
    /// <remarks>
    /// 场景：参考微软的eShopContainers项目中的做法。把领域事件发布延迟到上下文保存修改时。
    /// 实体中只是注册要发布的领域事件，然后在上下文中的SaveChanges方法被调用的是，在真正的发布事件
    /// </remarks>
    public interface IDomainEvents
    {
        /// <summary>
        /// 获取所有注册的领域事件
        /// </summary>
        /// <returns></returns>
        IEnumerable<INotification> GetDomainEvents();
        /// <summary>
        /// 注册领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEvent(INotification eventItem);
        /// <summary>
        /// 如果已经存在这个元素，则跳过，否则增加。以避免对于同样的事件触发多次（比如在一个事务中修改领域模型的多个对象）
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEventIfAbsent(INotification eventItem);
        /// <summary>
        /// 清楚消息的发布
        /// </summary>
        public void ClearDomainEvents();
    }
}
