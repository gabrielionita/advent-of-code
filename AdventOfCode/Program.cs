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
        public static async Task Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var dayType = GetDayType(args);
                logger.LogInformation($"Running {dayType.Name}");
                var instance = serviceProvider.GetRequiredService(dayType);
                var content = await ((Task<string>)dayType.GetMethod("GetStringContent")
                    .Invoke(instance, null))
                    .ConfigureAwait(false);

                var input = dayType.GetMethod("MapInput").Invoke(instance, new[] { content });
                var solveMethod = dayType.GetMethod("SolvePart1");
                var defaultSolution = Activator.CreateInstance(solveMethod.ReturnType);
                var solution = solveMethod.Invoke(instance, new[] { input });
                if (solution.Equals(defaultSolution))
                {
                    throw new Exception("No solution was found");
                }
                logger.LogInformation($"Solution for part 1: {solution}");

                solution = dayType.GetMethod("SolvePart2").Invoke(instance, new[] { input });
                if (solution.Equals(defaultSolution))
                {
                    throw new Exception("No solution was found");
                }
                logger.LogInformation($"Solution for part 2: {solution}");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error");
            }
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var services = new ServiceCollection();
            var startup = new Startup(configuration);
            startup.ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        private static Type GetDayType(string[] args)
        {
            Type dayType;
            if (!args.Any())
            {
                dayType = Assembly.Load("AdventOfCode.Days").ExportedTypes
                    .Where(type => !type.IsAbstract && type.Name.StartsWith("Day")).OrderByDescending(type => type.Name)
                    .First();
            }
            else if (args[0] == "--year" && int.TryParse(args[1], out var year) && args[2] == "--day" && int.TryParse(args[3], out var number))
            {
                dayType = Type.GetType($"AdventOfCode.Days{year}.Day{number:00}, AdventOfCode.Days", true);
            }
            else
            {
                throw new ArgumentException($"Specified arguments are invalid: {string.Join(", ", args)}", nameof(args));
            }
            return dayType;
        }
    }
}
