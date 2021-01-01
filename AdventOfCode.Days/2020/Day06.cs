using AdventOfCode.Abstractions;
using System;
using System.Linq;

namespace AdventOfCode.Days2020
{
	public class Day06 : DayBase<string[], int>
	{
		public override string[] MapInput(string input)
		{
			return input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
		}

		public override int SolvePart1(string[] groups)
		{
			return groups.Sum(group => group.Replace("\n", string.Empty).Distinct().Count());
		}

		public override int SolvePart2(string[] groups)
		{
			return groups.Sum(group =>
				group.Split("\n", StringSplitOptions.RemoveEmptyEntries)
					.Select(str => str.ToArray())
					.Aggregate((a, b) => a.Intersect(b).ToArray())
					.Length);
		}
	}
}
