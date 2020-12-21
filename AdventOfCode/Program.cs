using AdventOfCode.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
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
				await GetDay(args).Run();
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

		private static IRunnable GetDay(string[] args)
		{
			Type dayType;
			if (args[0] == "--latest-day")
			{
				dayType = Assembly.Load("AdventOfCode.Days").ExportedTypes
					.Where(type => !type.IsAbstract && type.Name.StartsWith("Day")).OrderByDescending(type => type.Name)
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
			return (IRunnable)serviceProvider.GetRequiredService(dayType);
		}
	}
}
