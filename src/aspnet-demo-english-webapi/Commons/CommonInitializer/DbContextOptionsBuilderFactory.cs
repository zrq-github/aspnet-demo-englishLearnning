using Microsoft.EntityFrameworkCore;

namespace CommonInitializer
{
    public static class DbContextOptionsBuilderFactory
    {
        /// <summary>
        /// 创建统一读取环境变量的设置
        /// </summary>
        /// <typeparam name="TDbContext"><see cref="DbContext"/></typeparam>
        /// <remarks>
        /// 从环境变量"DefaultDB:ConnStr"读取数据库的连接字符串
        /// 这也是各个服务所连接的数据库对象
        /// </remarks>
        /// <returns></returns>
        public static DbContextOptionsBuilder<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext
        {
            var connStr = Environment.GetEnvironmentVariable("DefaultDB:ConnStr");
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=YouzackVNextDB;User ID=sa;Password=dLLikhQWy5TBz1uM;");
            optionsBuilder.UseSqlServer(connStr);
            return optionsBuilder;
        }
    }
}
