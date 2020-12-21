using System;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days
{
	public class Day01 : DayBase
	{
		public Day01(HttpClient httpClient) : base(httpClient)
		{
		}

		private int[] InitData(string input)
		{
			return input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToArray();
		} 

		public override void SolvePart1(string input)
		{
			var data = InitData(input);

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

		public override void SolvePart2(string input)
		{
			var data = InitData(input);

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

