using AdventOfCode.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.HostedServices
{
	public class DayHostedService : BackgroundService
	{
		private readonly ILogger logger;
		private readonly IConfiguration configuration;
		private readonly IDayFactory dayFactory;
		private readonly IInputStorage inputStorage;

		public DayHostedService(ILogger<DayHostedService> logger, IConfiguration configuration, IDayFactory dayFactory, IInputStorage inputStorage)
		{
			this.logger = logger;
			this.configuration = configuration;
			this.dayFactory = dayFactory;
			this.inputStorage = inputStorage;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			var year = configuration.GetValue<int?>("Year");
			var day = configuration.GetValue<int?>("Day");
			try
			{
				var instance = dayFactory.Create(year, day);
				if (!year.HasValue || !day.HasValue)
				{
					var dayType = instance.GetType();
					day = int.Parse(dayType.Name.Substring(3));
					year = int.Parse(dayType.FullName.Substring(dayType.FullName.IndexOf("Days") + 4, 4));
				}

				logger.LogInformation("Running Day{day:00} of {year}", day, year);

				var content = await inputStorage.Read(year.Value, day.Value, cancellationToken);
				Execute((dynamic)instance, content);
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Error");
			}
		}

		private void Execute<TInput, TSolution>(DayBase<TInput, TSolution> day, string content)
		{
			foreach (var part in Enumerable.Range(1, 2))
			{
				var input = day.MapInput(content);
				var defaultSolution = default(TSolution);
				var solution = part == 1 ? day.SolvePart1(input) : day.SolvePart2(input);
				if (solution.Equals(defaultSolution))
				{
					throw new Exception("No solution was found");
				}
				logger.LogInformation("Solution for part {part}: {solution}", part, solution);
			}
		}
	}
}
