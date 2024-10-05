using System;

namespace Ron.DomainCommons.Models
{
    public interface IHasDeletionTime
    {
        DateTime? DeletionTime { get; }
    }
}
