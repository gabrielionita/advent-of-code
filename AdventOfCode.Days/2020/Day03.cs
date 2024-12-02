using System.Linq;

namespace AdventOfCode.Days2020
{
	public class Day03 : IDay<bool[][], int>
	{
		public bool[][] MapInput(string input)
		{
			var lines = input.Split('\n', System.StringSplitOptions.RemoveEmptyEntries).ToArray();
			var map = new bool[lines.Length][];
			for (var i = 0; i < lines.Length; i++)
			{
				map[i] = lines[i].Select(c => c == '#').ToArray();
			}

			return map;
		}

		private int Slope(bool[][] map, int right, int down)
		{
			var treesEncountered = 0;
			var i = 0;
			var j = 0;
			var columns = map[0].Length;
			while (i < map.Length)
			{
				if (map[i][j])
				{
					treesEncountered++;
				}
				i += down;
				j = (j + right) % columns;
			}

			return treesEncountered;
		}

		public int SolvePart1(bool[][] map)
		{
			return Slope(map, 3, 1);
		}

		public int SolvePart2(bool[][] map)
		{
			return Slope(map, 1, 1) * Slope(map, 3, 1) * Slope(map, 5, 1) * Slope(map, 7, 1) * Slope(map, 1, 2);
		}
	}
}
