using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2022
{
	public class Day01 : IDay<List<List<int>>, int>
	{
		public List<List<int>> MapInput(string input)
		{
			var lines = input.Split('\n');
			var caloriesByElves = new List<List<int>>
			{
				new()
			};
			foreach (var line in lines)
			{
				if (string.IsNullOrEmpty(line))
				{
					caloriesByElves.Add(new List<int>());
				}
				else
				{
					var calories = int.Parse(line);
					caloriesByElves[^1].Add(calories);
				}
			}

			return caloriesByElves;
		}

		public int SolvePart1(List<List<int>> input)
		{
			return input.Max(elf => elf.Sum());
		}

		public int SolvePart2(List<List<int>> input)
		{
			return input.Select(elf => elf.Sum()).OrderByDescending(sum => sum).Take(3).Sum();
		}
	}
}
