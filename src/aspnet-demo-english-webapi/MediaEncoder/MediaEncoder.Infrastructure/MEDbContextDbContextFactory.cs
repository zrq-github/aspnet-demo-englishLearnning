using CommonInitializer;
using MediaEncoder.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace Listening.Infrastructure
{
    public class MEDbContextDbContextFactory : IDesignTimeDbContextFactory<MEDbContext>
    {
        public MEDbContext CreateDbContext(string[] args) 
        {
            var dbOptions = DbContextOptionsBuilderFactory.Create<MEDbContext>();
            return new MEDbContext(dbOptions.Options,null);
        }
    }
}