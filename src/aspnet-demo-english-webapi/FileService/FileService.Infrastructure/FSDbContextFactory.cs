using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInitializer;

namespace FileService.Infrastructure
{
    public class FSDbContextFactory : IDesignTimeDbContextFactory<FSDbContext>
    {
        public FSDbContext CreateDbContext(string[] args) 
        {
            var dbOptions = DbContextOptionsBuilderFactory.Create<FSDbContext>();
            return new FSDbContext(dbOptions.Options, null);
        }
    }

}
