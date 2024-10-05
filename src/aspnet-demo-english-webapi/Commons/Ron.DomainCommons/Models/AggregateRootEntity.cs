using System;

namespace Ron.DomainCommons.Models
{
    /// <summary>
    /// 聚合实体
    /// </summary>
    public record AggregateRootEntity : BaseEntity, IAggregateRoot, ISoftDelete, IHasCreationTime, IHasDeletionTime, IHasModificationTime
    {
        public bool IsDeleted { get; private set; }
        public DateTime CreationTime { get; private set; } = DateTime.Now;
        public DateTime? DeletionTime { get; private set; }
        public DateTime? LastModificationTime { get; private set; }

        public virtual void SoftDelete()
        {
            this.IsDeleted = true;
            this.DeletionTime = DateTime.Now;
        }

        public void NotifyModified()
        {
            this.LastModificationTime = DateTime.Now;
        }
    }
}
