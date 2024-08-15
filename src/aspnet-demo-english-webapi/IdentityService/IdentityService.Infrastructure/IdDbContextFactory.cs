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
    public class IdDbContextFactory : IDesignTimeDbContextFactory<IdDbContext>
    {
        public IdDbContext CreateDbContext(string[] args) 
        {
            var dbOptions = DbContextOptionsBuilderFactory.Create<IdDbContext>();
            return new IdDbContext(dbOptions.Options);
        }
    }

}
