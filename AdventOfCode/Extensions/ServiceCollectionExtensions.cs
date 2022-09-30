using AdventOfCode.HostedServices;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDays(this IServiceCollection services)
        {
            var dayTypes = Assembly.Load("AdventOfCode.Days").ExportedTypes
                    .Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface);

            foreach (var type in dayTypes)
                services.AddTransient(type);

            return services;
        }

        public static IServiceCollection AddHostedService(this IServiceCollection services, bool download)
        {
            if (download)
                services.AddHostedService<InputDownloadHostedService>();
            else
                services.AddHostedService<DayHostedService>();

            return services;
        }
    }
}
