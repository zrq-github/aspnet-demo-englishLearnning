using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInitializer;
using DBConfig;

namespace FileService.Infrastructure
{
    public class DbConfigContextFactory : IDesignTimeDbContextFactory<DbConfigContext>
    {
        public DbConfigContext CreateDbContext(string[] args) 
        {
            var dbOptions = DbContextOptionsBuilderFactory.Create<DbConfigContext>();
            return new DbConfigContext(dbOptions.Options);
        }
    }

}
