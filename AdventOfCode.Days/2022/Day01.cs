using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2022
{
	public class Day01 : DayBase<List<List<int>>, int>
	{
		public override List<List<int>> MapInput(string input)
		{
			var lines = input.Split('\n');
			var caloriesByElves = new List<List<int>>
			{
				new List<int>()
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

		public override int SolvePart1(List<List<int>> input)
		{
			return input.Max(elf => elf.Sum());
		}

		public override int SolvePart2(List<List<int>> input)
		{
			return input.Select(elf => elf.Sum()).OrderByDescending(sum => sum).Take(3).Sum();
		}
	}
}
