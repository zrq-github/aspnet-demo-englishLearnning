using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain
{
    /// <summary>
    /// 在系统中的角色
    /// </summary>
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
