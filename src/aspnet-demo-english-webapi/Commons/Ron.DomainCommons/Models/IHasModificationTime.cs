using System;

namespace Ron.DomainCommons.Models
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; }

    }
}
