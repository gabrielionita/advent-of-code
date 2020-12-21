using AdventOfCode.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days
{
	public class Day01 : DayBase<int[]>
	{
		public Day01(HttpClient httpClient, ILogger<Day01> logger) : base(httpClient, logger)
		{
		}

		protected override int[] MapInput(string input)
		{
			return input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToArray();
		}

		protected override void SolvePart1(int[] data)
		{
			for (var i = 0; i < data.Length - 1; i++)
			{
				for (var j = i; j < data.Length; j++)
				{
					if (data[i] + data[j] == 2020)
					{
						var multiplication = data[i] * data[j];
						Solution = multiplication;
						break;
					}
				}
			}
		}

		protected override void SolvePart2(int[] data)
		{
			for (var i = 0; i < data.Length - 2; i++)
			{
				for (var j = i + 1; j < data.Length - 1; j++)
				{
					for (var k = j + 1; k < data.Length; k++)
					{
						if (data[i] + data[j] + data[k] == 2020)
						{
							var multiplication = data[i] * data[j] * data[k];
							Solution = multiplication;
							break;
						}
					}
				}
			}
		}
	}
}

