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
		private static ILogger logger;
		private static IServiceProvider serviceProvider;

        public static async Task Main(string[] args)
		{
			Init();

			try
			{
				var day = GetDay(args);
				await Run(logger, day);
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Error");
			}
		}

		private static void Init()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", false)
				.Build();

			var services = new ServiceCollection();
			var startup = new Startup(configuration);
			startup.ConfigureServices(services);
			serviceProvider = services.BuildServiceProvider();
			logger = serviceProvider.GetRequiredService<ILogger<Program>>();
		}

		private static DayBase GetDay(string[] args)
		{
			Type dayType;
			if (args[0] == "--latest-day")
			{
				dayType = typeof(DayBase).Assembly.GetTypes()
					.Where(type => type.Name != nameof(DayBase) && type.Name.StartsWith("Day")).OrderByDescending(type => type.Name)
					.First();
			}
			else if (args[0] == "--day" && int.TryParse(args[1], out var number))
			{
				dayType = Type.GetType($"AdventOfCode.Days.Day{number:00}, AdventOfCode.Days", true);
			}
			else
			{
				throw new ArgumentException("Args are not specified");
			}
			logger.LogInformation($"Running {dayType.Name}");
			return (DayBase)serviceProvider.GetRequiredService(dayType);
		}

		private static async Task Run(ILogger logger, DayBase day)
		{
			var input = await day.GetInput();
			day.SolvePart1(input);
			if (day.Solution == null)
			{
				throw new SolutionNotFoundException();
			}
			logger.LogInformation($"Solution for part 1: {day.Solution}");

			day.SolvePart2(input);
			if (day.Solution == null)
			{
				throw new SolutionNotFoundException();
			}
			logger.LogInformation($"Solution for part 2: {day.Solution}");
		}
	}
}
