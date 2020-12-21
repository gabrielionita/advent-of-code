﻿using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days
{
	public class Day03 : DayBase
	{
		public Day03(HttpClient httpClient) : base(httpClient)
		{
		}

		private bool[][] InitData(string input)
		{
			var lines = input.Split('\n', System.StringSplitOptions.RemoveEmptyEntries).ToArray();
			var map = new bool[lines.Length][];
			for (var i = 0; i < lines.Length; i++)
			{
				map[i] = lines[i].Select(c => c == '#').ToArray();
			}

			return map;
		}

		public override void SolvePart1(string input)
		{
			var map = InitData(input);
			var treesEncountered = Slope(map, 3, 1);
			Solution = treesEncountered;
		}

		private long Slope(bool[][] map, int right, int down)
		{
			var treesEncountered = 0;
			var i = 0;
			var j = 0;
			var columns = map[0].Length;
			while (i <  map.Length)
			{
				if (map[i][j])
				{
					treesEncountered++;
				}
				i = i + down;
				j = (j + right) % columns;
			}

			return treesEncountered;
		}

		public override void SolvePart2(string input)
		{
			var map = InitData(input);
			var treesEncounteder = Slope(map, 1, 1) * Slope(map, 3, 1) * Slope(map, 5, 1) * Slope(map, 7, 1) * Slope(map, 1, 2);
			Solution = treesEncounteder;
		}
	}
}
