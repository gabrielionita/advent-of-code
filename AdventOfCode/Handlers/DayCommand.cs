using AdventOfCode.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Handlers
{
    [Command, Subcommand(typeof(DownloadCommand))]
    public class DayCommand
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly DayFactory dayFactory;
        private readonly InputStorage inputStorage;

        [Option]
        public int? Year { get; set; }
        [Option]
        public int? Day { get; set; }

        public DayCommand(ILogger<DayCommand> logger, IConfiguration configuration, DayFactory dayFactory, InputStorage inputStorage)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.dayFactory = dayFactory;
            this.inputStorage = inputStorage;
        }

        public async Task OnExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                var dayType = GetDayType(Year, Day);
                if (!Year.HasValue || !Day.HasValue)
                {
                    Day = int.Parse(dayType.Name.Substring(3));
                    Year = int.Parse(dayType.FullName.Substring(dayType.FullName.IndexOf("Days") + 4, 4));
                }


                logger.LogInformation("Running {day} of {year}", dayType.Name, Year);

                dynamic instance = dayFactory.Create(dayType);
                var content = await inputStorage.Read(Year.Value, Day.Value, cancellationToken);
                Execute(instance, content);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error");
            }
        }

        private Type GetDayType(int? year, int? day)
        {
            if (year.HasValue && day.HasValue)
            {
                return Type.GetType($"AdventOfCode.Days{year}.Day{day:00}, AdventOfCode.Days", true);
            }

            return typeof(Days2020.Day01).Assembly.ExportedTypes
                .Where(type => !type.IsAbstract && type.Name.StartsWith("Day"))
                .OrderByDescending(type => type.FullName)
                .FirstOrDefault();
        }

        private void Execute<TInput, TSolution>(DayBase<TInput, TSolution> day, string content)
        {
            var input = day.MapInput(content);
            var defaultSolution = default(TSolution);
            var solution = day.SolvePart1(input);
            if (solution.Equals(defaultSolution))
            {
                throw new Exception("No solution was found");
            }
            logger.LogInformation("Solution for part {part}: {solution}", 1, solution);

            input = day.MapInput(content);
            solution = day.SolvePart2(input);
            if (solution.Equals(defaultSolution))
            {
                throw new Exception("No solution was found");
            }
            logger.LogInformation("Solution for part {part}: {solution}", 2, solution);
        }
    }
}
