using CommonInitializer;
using Listening.Domain;
using Microsoft.EntityFrameworkCore.Design;

namespace Listening.Infrastructure
{
    public class IdDbContextFactory : IDesignTimeDbContextFactory<ListeningDbContext>
    {
        public ListeningDbContext CreateDbContext(string[] args) 
        {
            var dbOptions = DbContextOptionsBuilderFactory.Create<ListeningDbContext>();
            return new ListeningDbContext(dbOptions.Options,null);
        }
    }
}