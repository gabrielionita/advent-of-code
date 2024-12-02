using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day08 : IDay<string[][], int>
	{
		public string[][] MapInput(string input)
		{
			return input.Split('\n').Select(c => c.Split(" ")).ToArray();
		}

		public int SolvePart1(string[][] input)
		{
			var acceptedLengths = new int[] { 2, 3, 4, 7 };
			var count = 0;

			foreach (var line in input)
				foreach (var signal in line.TakeLast(4))
					if (acceptedLengths.Contains(signal.Length))
						count++;

			return count;
		}

		public int SolvePart2(string[][] input)
		{
			var sum = 0;
			foreach (var line in input)
			{
				var dictionary = new Dictionary<int, string>(10);
				foreach (var signal in line.SkipLast(5).OrderBy(s => s.Length))
				{
					var figure = -1;
					var orderedSignal = new string(signal.OrderBy(c => c).ToArray());
					if (orderedSignal.Length == 2)
						figure = 1;
					else if (orderedSignal.Length == 3)
						figure = 7;
					else if (orderedSignal.Length == 4)
						figure = 4;
					else if (orderedSignal.Length == 7)
						figure = 8;
					else if (orderedSignal.Length == 5)
					{
						if (dictionary[1].Intersect(orderedSignal).SequenceEqual(dictionary[1]))
							figure = 3;
						else if (dictionary[4].Intersect(orderedSignal).Count() == 2)
							figure = 2;
						else if (dictionary[4].Intersect(orderedSignal).Count() == 3)
							figure = 5;
					}
					else if (orderedSignal.Length == 6)
					{
						if (dictionary[1].Intersect(orderedSignal).Count() == 1)
							figure = 6;

						else if (dictionary[4].Intersect(orderedSignal).SequenceEqual(dictionary[4]))
							figure = 9;
						else
							figure = 0;
					}

					dictionary[figure] = orderedSignal;
				}

				var number = 0;
				var position = 3;
				foreach (var signal in line.TakeLast(4))
				{
					var orderedSignal = new string(signal.OrderBy(c => c).ToArray());
					var figure = dictionary.FirstOrDefault(kv => kv.Value == orderedSignal).Key;
					number += figure * (int)Math.Pow(10, position);
					position--;
				}

				sum += number;
			}

			return sum;
		}
	}
}
