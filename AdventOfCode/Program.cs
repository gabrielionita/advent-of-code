using AdventOfCode.Days;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var services = new ServiceCollection();
            var startup = new Startup(configuration);
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var day = serviceProvider.GetRequiredService<Day01>();
            await day.Run();
        }
    }
}
