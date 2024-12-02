using System;
using System.Linq;

namespace AdventOfCode.Days2020
{
	public class Day09 : IDay<long[], long>
	{
		private const int preambleLength = 25;

		public long[] MapInput(string input)
		{
			return input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(line => long.Parse(line)).ToArray();
		}

		public long SolvePart1(long[] numbers)
		{
			return FindInvalidNumber(numbers);
		}

		private static long FindInvalidNumber(long[] numbers, bool returnIndex = false)
		{
			for (var i = preambleLength; i < numbers.Length - 1; i++)
			{
				var preamble = numbers[(i - preambleLength)..i];

				var valid = false;
				for (var j = 0; j < preambleLength - 1; j++)
				{
					for (var k = j + 1; k < preambleLength; k++)
					{
						if (numbers[i] == preamble[j] + preamble[k])
						{
							valid = true;
							break;
						}
					}
					if (valid)
					{
						break;
					}
				}

				if (!valid)
				{
					return returnIndex ? i : numbers[i];
				}
			}
			return 0;
		}

		public long SolvePart2(long[] numbers)
		{
			var invalidNumberIndex = (int)FindInvalidNumber(numbers, true);
			var invalidNumber = numbers[invalidNumberIndex];

			var i = 0;
			var j = 1;
			while (i < invalidNumberIndex)
			{
				var set = numbers.Skip(i).Take(j - i);
				var sum = set.Sum();
				if (sum == invalidNumber)
				{
					return set.Min() + set.Max();
				}

				if (sum < invalidNumber)
				{
					j++;
				}
				else
				{
					i++;
				}
			}

			return 0;
		}
	}
}
