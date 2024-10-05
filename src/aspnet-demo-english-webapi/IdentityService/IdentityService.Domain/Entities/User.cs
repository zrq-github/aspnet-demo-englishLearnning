using Microsoft.AspNetCore.Identity;
using Ron.DomainCommons.Models;

namespace IdentityService.Domain;
public class User : IdentityUser<Guid>, IHasCreationTime, IHasDeletionTime, ISoftDelete
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; init; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletionTime { get; private set; }

    /// <summary>
    /// 是否是软删除
    /// </summary>
    public bool IsDeleted { get; private set; }

    public User(string userName) : base(userName)
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public void SoftDelete()
    {
        this.IsDeleted = true;
        this.DeletionTime = DateTime.Now;
    }
}
