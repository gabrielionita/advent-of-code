using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Abstractions
{
	public abstract class DayBase<TInput, TSolution> : IRunnable
	{
		private readonly HttpClient httpClient;
		private readonly ILogger logger;
		private readonly int day;

		protected DayBase(HttpClient httpClient, ILogger logger)
		{
			this.httpClient = httpClient;
			this.logger = logger;
			day = int.Parse(GetType().Name[3..]);
		}

		protected virtual async Task<string> GetStringContent()
		{
			var response = await httpClient.GetAsync($"day/{day}/input");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}

		protected abstract TInput MapInput(string input);

		protected abstract TSolution SolvePart1(TInput input);

		protected abstract TSolution SolvePart2(TInput input);

		public async Task Run()
		{
			var content = await GetStringContent();
			var input = MapInput(content);
			var solution = SolvePart1(input);
			if (solution.Equals(default(TSolution)))
			{
				throw new Exception("No solution was found");
			}
			logger.LogInformation($"Solution for part 1: {solution}");

			solution = SolvePart2(input);
			if (solution.Equals(default(TSolution)))
			{
				throw new Exception("No solution was found");
			}
			logger.LogInformation($"Solution for part 2: {solution}");
		}
	}
}
