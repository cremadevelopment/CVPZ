using CVPZ.Application.Job;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CVPZ.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<JobCountService>();
        return services;
    }
}