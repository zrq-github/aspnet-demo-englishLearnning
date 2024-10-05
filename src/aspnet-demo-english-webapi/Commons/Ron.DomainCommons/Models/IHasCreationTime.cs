using System;

namespace Ron.DomainCommons.Models
{
    /// <summary>
    /// 是否有创建时间的接口
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; }
    }
}
