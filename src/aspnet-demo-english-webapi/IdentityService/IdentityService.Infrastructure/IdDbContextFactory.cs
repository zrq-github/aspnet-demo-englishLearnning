using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInitializer;
using IdentityService.Infrastructure;

namespace FileService.Infrastructure
{
    /// <summary>
    /// EntityFrameworkCore.Tools 所用，不应该被实际代码调用
    /// </summary>
    [Obsolete("This class should not be used, it is tool class", true)]
    public class IdDbContextFactory : IDesignTimeDbContextFactory<IdDbContext>
    {
        public IdDbContext CreateDbContext(string[] args) 
        {
            var dbOptions = DbContextOptionsBuilderFactory.Create<IdDbContext>();
            return new IdDbContext(dbOptions.Options);
        }
    }

}
