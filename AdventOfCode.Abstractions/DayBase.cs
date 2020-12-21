using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Abstractions
{
	public abstract class DayBase<TInput> : IRunnable
	{
		private readonly HttpClient httpClient;
		private readonly ILogger logger;
		private readonly int day;

		protected object Solution { get; set; }

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

		protected abstract void SolvePart1(TInput input);

		protected abstract void SolvePart2(TInput input);

		public async Task Run()
		{
			var content = await GetStringContent();
			var input = MapInput(content);
			SolvePart1(input);
			if (Solution == null)
			{
				throw new Exception("No solution was found");
			}
			logger.LogInformation($"Solution for part 1: {Solution}");

			SolvePart2(input);
			if (Solution == null)
			{
				throw new Exception("No solution was found");
			}
			logger.LogInformation($"Solution for part 2: {Solution}");
		}
	}
}
