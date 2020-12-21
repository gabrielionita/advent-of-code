using AdventOfCode.Days;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Program
    {
		private static DayBase day;
		private static ILogger logger;

        public static async Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var services = new ServiceCollection();
            var startup = new Startup(configuration);
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Test");
            var newestDayType = typeof(DayBase).Assembly.GetTypes()
                .Where(type => type.Name != nameof(DayBase) && type.Name.StartsWith("Day")).OrderByDescending(type => type.Name)
                .First();
            day = (DayBase)serviceProvider.GetRequiredService(newestDayType);
            logger.LogInformation($"Running {newestDayType.Name}");

            try
			{
				await Run();
			}
			catch (Exception exception)
			{
                logger.LogError(exception, "Error");
			}
        }

		private static async Task Run()
		{
			var input = await day.GetInput();
			day.SolvePart1(input);
			if (string.IsNullOrEmpty(day.Solution))
			{
				throw new SolutionNotFoundException();
			}
			logger.LogInformation($"Solution for part 1: {day.Solution}");

			day.SolvePart2(input);
			if (string.IsNullOrEmpty(day.Solution))
			{
				throw new SolutionNotFoundException();
			}
			logger.LogInformation($"Solution for part 2: {day.Solution}");
		}
	}
}
