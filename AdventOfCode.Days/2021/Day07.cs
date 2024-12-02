using System;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day07 : IDay<int[], int>
	{
		public int[] MapInput(string input)
		{
			return input.Split(',').Select(int.Parse).ToArray();
		}

		public int SolvePart1(int[] input)
		{
			var array = input.OrderBy(i => i).ToArray();
			var median = array.Length % 2 == 1 ? array[array.Length / 2] : (array[(array.Length + 1) / 2] + array[(array.Length - 1) / 2]) / 2;
			return array.Sum(i => Math.Abs(i - median));
		}

		public int SolvePart2(int[] input)
		{
			var minimumSum = int.MaxValue;
			var maxValue = input.Max();

			for (var i = 0; i < maxValue; i++)
			{
				var sum = 0;
				foreach (var number in input)
				{
					var min = Math.Min(number, i);
					var max = Math.Max(number, i);
					for (var j = min; j < max; j++)
						sum += j - min + 1;
				}

				if (minimumSum > sum)
					minimumSum = sum;
			}

			return minimumSum;
		}
	}
}
