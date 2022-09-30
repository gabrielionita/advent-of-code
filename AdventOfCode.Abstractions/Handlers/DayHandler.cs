using AdventOfCode.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Handlers
{
    public class DayHandler
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly IDayFactory dayFactory;
        private readonly IInputStorage inputStorage;

        public DayHandler(ILogger<DayHandler> logger, IConfiguration configuration, IDayFactory dayFactory, IInputStorage inputStorage)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.dayFactory = dayFactory;
            this.inputStorage = inputStorage;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            var year = configuration.GetValue<int?>("Year");
            var day = configuration.GetValue<int?>("Day");
            try
            {
                dynamic instance = dayFactory.Create(year, day);
                if (!year.HasValue || !day.HasValue)
                {
                    var dayType = instance.GetType();
                    day = int.Parse(dayType.Name.Substring(3));
                    year = int.Parse(dayType.FullName.Substring(dayType.FullName.IndexOf("Days") + 4, 4));
                }

                logger.LogInformation("Running Day{day:00} of {year}", day, year);
                
                var content = await inputStorage.Read(year.Value, day.Value, cancellationToken);
                Execute(instance, content);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error");
            }
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
