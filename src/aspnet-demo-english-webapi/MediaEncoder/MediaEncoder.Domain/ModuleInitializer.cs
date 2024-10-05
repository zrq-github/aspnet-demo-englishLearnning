using Microsoft.Extensions.DependencyInjection;
using Ron.Commons;

namespace MediaEncoder.Domain;
public class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<MediaEncoderFactory>();
    }
}
