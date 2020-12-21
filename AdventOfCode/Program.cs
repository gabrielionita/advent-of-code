using AdventOfCode.Days;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
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

            var newestDayType = typeof(DayBase).Assembly.GetTypes()
                .Where(type => type.Name != nameof(DayBase) && type.Name.StartsWith("Day")).OrderByDescending(type => type.Name)
                .First();
            var day = (DayBase)serviceProvider.GetRequiredService(newestDayType);
            Console.WriteLine($"Running {newestDayType.Name}");
            try
            {
                await day.Run();
            }
            catch (Exception exception)
			{
                Console.Error.WriteLine(exception.Message);
			}
        }
    }
}
