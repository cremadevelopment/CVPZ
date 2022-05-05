using CVPZ.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVPZ.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cvpzDatabaseConnectionString = configuration.GetConnectionString("CVPZ");
        services.AddDbContext<CVPZContext>(options => options.UseSqlServer(cvpzDatabaseConnectionString));

        return services;
    }
}