using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2020
{
	public class Day10 : DayBase<int[], long>
	{
		public override int[] MapInput(string input)
		{
			return input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(line => int.Parse(line)).ToArray();
		}

		public override long SolvePart1(int[] jolts)
		{
			var difference1Jolt = 0;
			var difference2Jolt = 0;
			var difference3Jolt = 1;
			jolts = jolts.OrderBy(jolt => jolt).ToArray();
			var currentJolt = 0;
			foreach (var jolt in jolts)
			{
				var difference = jolt - currentJolt;
				if (difference == 1)
				{
					difference1Jolt++;
				}
				else if (difference == 2)
				{
					difference2Jolt++;
				}
				else if (difference == 3)
				{
					difference3Jolt++;
				}
				currentJolt = jolt;
			}

			return difference1Jolt * difference3Jolt;
		}

		public override long SolvePart2(int[] jolts)
		{
			var list = jolts.OrderBy(jolt => jolt).ToList();
			list.Add(list[^1] + 3);
			list.Insert(0, 0);
			jolts = list.ToArray();

			var deltas = new int[jolts.Length];
            for (var i = 1; i < jolts.Length; i++)
			{
				deltas[i] = jolts[i] - jolts[i - 1];
			}

			var continuousOnes = new List<int>();

			var ones = 0;
			foreach(var delta in deltas)
			{
				if (delta == 1)
				{
					ones++;
					continue;
				}

				if (ones == 0)
					continue;

				continuousOnes.Add(ones);
				ones = 0;
			}

			return continuousOnes.Aggregate(1L, (sum, sequenceLength) => sum *= Tribonacci(sequenceLength + 3));
        }

        public long Tribonacci(int n)
        {
            if (n is 0 or 1 or 2)
                return 0;

            if (n == 3)
                return 1;

            return Tribonacci(n - 1) + Tribonacci(n - 2) + Tribonacci(n - 3);
        }
    }
}
