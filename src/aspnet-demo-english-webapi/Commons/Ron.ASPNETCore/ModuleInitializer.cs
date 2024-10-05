using Microsoft.Extensions.DependencyInjection;
using Ron.Commons;

namespace Ron.ASPNETCore
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
            services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();
        }
    }
}
