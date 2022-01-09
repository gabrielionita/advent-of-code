using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Program
	{
		private const string MapInputMethodName = "MapInput";
		private const string SolvePart1MethodName = "SolvePart1";
		private const string SolvePart2MethodName = "SolvePart2";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="year">The year of the Advent of Code contest. If not specified, it will be the latest implemented.</param>
		/// <param name="day">The day to run. If not specified, it will be the latest implemented.</param>
		/// <returns>Test </returns>
		public static async Task Main(int? year, int? day)
		{
			var serviceProvider = BuildServiceProvider();
			var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
			var adventOfCodeClient = serviceProvider.GetRequiredService<AdventOfCodeClient>();

			try
			{
				var dayType = GetDayType(year, day)!;
				logger.LogInformation("Running {dayTypeName}", dayType.Name);
				var instance = serviceProvider.GetRequiredService(dayType);

				if(!year.HasValue || !day.HasValue)
                {
					day = int.Parse(dayType.Name.Substring(3));
					year = int.Parse(dayType.FullName.Substring(dayType.FullName.IndexOf("Days") + 4, 4));
				}

				var content = await adventOfCodeClient.GetInput(year.Value, day.Value);

				var input = dayType.GetMethod(MapInputMethodName).Invoke(instance, new[] { content });
				var solveMethod = dayType.GetMethod(SolvePart1MethodName);
				var defaultSolution = Activator.CreateInstance(solveMethod.ReturnType);

				var solution = solveMethod.Invoke(instance, new[] { input });
				if (solution.Equals(defaultSolution))
				{
					throw new Exception("No solution was found");
				}
				logger.LogInformation("Solution for part 1: {solution}", solution);

				input = dayType.GetMethod(MapInputMethodName).Invoke(instance, new[] { content });
				solution = dayType.GetMethod(SolvePart2MethodName).Invoke(instance, new[] { input });
				if (solution.Equals(defaultSolution))
				{
					throw new Exception("No solution was found");
				}
				logger.LogInformation("Solution for part 2: {solution}", solution);
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Error");
			}
		}

		private static IServiceProvider BuildServiceProvider()
		{
			var configuration = new ConfigurationBuilder()
				.AddUserSecrets<Program>()
				.Build();

			var services = new ServiceCollection();
			var startup = new Startup(configuration);
			startup.ConfigureServices(services);
			return services.BuildServiceProvider();
		}

		private static Type GetDayType(int? year, int? day)
		{
			if (year.HasValue && day.HasValue)
			{
				return Type.GetType($"AdventOfCode.Days{year}.Day{day:00}, AdventOfCode.Days", true);
			}

			return Assembly.Load("AdventOfCode.Days").ExportedTypes
				.Where(type => !type.IsAbstract && type.Name.StartsWith("Day"))
				.OrderByDescending(type => type.FullName)
				.FirstOrDefault();
		}
	}
}
