using System;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days
{
	public class Day02 : DayBase
	{
		public Day02(HttpClient httpClient) : base(httpClient)
		{
		}

		public override void SolvePart1(string input)
		{
			var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
			var validPasswords = 0;
			foreach(var line in lines)
			{
				var groups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var firstGroup = groups[0].Split('-');
				var minimum = int.Parse(firstGroup[0]);
				var maximum = int.Parse(firstGroup[1]);
				var letter = groups[1][0];
				var password = groups[2];

				var letterFrequency = password.Count(c => c == letter);
				if (password.Contains(letter) && letterFrequency >= minimum && letterFrequency <= maximum)
				{
					validPasswords++;
				}
			}

			Solution = validPasswords.ToString();
		}

		public override void SolvePart2(string input)
		{
			var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
			var validPasswords = 0;
			foreach (var line in lines)
			{
				var groups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var firstGroup = groups[0].Split('-');
				var firstPosition = int.Parse(firstGroup[0]) - 1;
				var secondPosition = int.Parse(firstGroup[1]) - 1;
				var letter = groups[1][0];
				var password = groups[2];

				if (password.Contains(letter) && (password[firstPosition] == letter ^ password[secondPosition] == letter))
				{
					validPasswords++;
				}
			}

			Solution = validPasswords.ToString();
		}
	}
}
