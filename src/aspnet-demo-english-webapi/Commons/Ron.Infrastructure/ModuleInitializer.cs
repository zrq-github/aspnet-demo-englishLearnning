﻿using Microsoft.Extensions.DependencyInjection;
using Ron.Commons;

namespace Ron.Infrastructure
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            //因为是泛型，所以不能用AddScoped<IDbContextRepository<>,typeof(DbContextRepository<>>这种方式注册
            //services.AddScoped(typeof(IDbContextRepository<>), typeof(DbContextRepository<>));
        }
    }
}
