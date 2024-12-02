using System;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day01 : IDay<int[], int>
	{
		public int[] MapInput(string input) => input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

		public int SolvePart1(int[] input) => GenericSolution(input, 1);

		public int SolvePart2(int[] input) => GenericSolution(input, 3);

		private int GenericSolution(int[] input, int windowSize)
		{
			var increases = 0;
			var previousSum = int.MaxValue;
			for (var i = windowSize - 1; i < input.Length; i++)
			{
				var sum = 0;
				for (var j = 0; j < windowSize; j++)
					sum += input[i - j];

				if (previousSum < sum)
					increases++;

				previousSum = sum;
			}

			return increases;
		}
	}
}
