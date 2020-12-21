using AdventOfCode.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days
{
	public class Day06 : DayBase<string[], int>
	{
		public Day06(HttpClient httpClient, ILogger<Day06> logger) : base(httpClient, logger)
		{
		}

		protected override string[] MapInput(string input)
		{
			return input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
		}

		protected override int SolvePart1(string[] groups)
		{
			return groups.Sum(group => group.Replace("\n", string.Empty).Distinct().Count());
		}

		protected override int SolvePart2(string[] groups)
		{
			return groups.Sum(group =>
				group.Split("\n", StringSplitOptions.RemoveEmptyEntries)
					.Select(str => str.ToArray())
					.Aggregate((a, b) => a.Intersect(b).ToArray())
					.Length);
		}
	}
}
