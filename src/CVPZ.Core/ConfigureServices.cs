using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVPZ.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddOptions<UserConfiguration>()
            .Configure<IConfiguration>((settings, configuration) => {
                configuration.GetSection(UserConfiguration.SectionName).Bind(settings);
            });

        return services;
    }
}
