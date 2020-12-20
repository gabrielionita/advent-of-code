using System;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode
{
	public class Day1 : DayBase
	{
		public Day1(IHttpClientFactory httpClientFactory) : base(httpClientFactory, 1)
		{

		}

		protected override void SolvePart1(string input)
		{
			var data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToArray();

			var solution = string.Empty;

			for (var i = 0; i < data.Length - 1; i++)
			{
				for (var j = i; j < data.Length; j++)
				{
					if (data[i] + data[j] == 2020)
					{
						var multiplication = data[i] * data[j];
						solution = multiplication.ToString();
						break;
					}
				}
			}
		}

		protected override void SolvePart2(string input)
		{
			var data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToArray();

			var solution = string.Empty;

			for (var i = 0; i < data.Length - 2; i++)
			{
				for (var j = i + 1; j < data.Length - 1; j++)
				{
					for (var k = j + 1; k < data.Length; k++)
					{
						if (data[i] + data[j] + data[k] == 2020)
						{
							var multiplication = data[i] * data[j] * data[k];
							solution = multiplication.ToString();
							break;
						}
					}
				}
			}
		}
	}
}

