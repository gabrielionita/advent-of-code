using System;
using System.Drawing;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day05 : DayBase<DataDay05[], int>
	{
		public override DataDay05[] MapInput(string input)
		{
			var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
			var data = new DataDay05[lines.Length];
			for (var i = 0; i < lines.Length; i++)
			{
				var lineSplit = lines[i].Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
				var pointSplit = lineSplit.SelectMany(c => c.Split(',').Select(int.Parse)).ToArray();
				data[i] = new DataDay05(pointSplit[0], pointSplit[1], pointSplit[2], pointSplit[3]);
			}
			return data;
		}

		public override int SolvePart1(DataDay05[] input)
		{
			return Solve(input, false);
		}

		public override int SolvePart2(DataDay05[] input)
		{
			return Solve(input, true);
		}

		private int Solve(DataDay05[] input, bool handleDiagonalLines)
		{
			var x1Max = input.Max(c => c.Start.X);
			var y1Max = input.Max(c => c.Start.Y);
			var x2Max = input.Max(c => c.End.X);
			var y2Max = input.Max(c => c.End.Y);

			var map = new int[Math.Max(x1Max, x2Max) + 1, Math.Max(y1Max, y2Max) + 1];
			foreach (var item in input)
			{
				var xMin = Math.Min(item.Start.X, item.End.X);
				var xMax = Math.Max(item.Start.X, item.End.X);
				var yMin = Math.Min(item.Start.Y, item.End.Y);
				var yMax = Math.Max(item.Start.Y, item.End.Y);

				if (item.Start.X == item.End.X)
					for (var i = yMin; i <= yMax; i++)
						map[item.Start.X, i]++;
				else if (item.Start.Y == item.End.Y)
					for (var i = xMin; i <= xMax; i++)
						map[i, item.Start.Y]++;
				else if (handleDiagonalLines)
				{
					var slope = (item.End.Y - item.Start.Y) / (double)(item.End.X - item.Start.X);
					if (slope > 0)
						for (var i = xMin; i <= xMax; i++)
							map[i, yMin + (i - xMin)]++;
					else
						for (var i = xMin; i <= xMax; i++)
							map[i, yMax - (i - xMin)]++;
				}
			}

			var count = 0;
			for (var i = 0; i < map.GetLength(0); i++)
				for (var j = 0; j < map.GetLength(1); j++)
					if (map[i, j] > 1)
						count++;
			return count;
		}
	}

	public class DataDay05
	{
		public Point Start { get; }
		public Point End { get; }

		public DataDay05(int x1, int y1, int x2, int y2)
		{
			Start = new Point(x1, y1);
			End = new Point(x2, y2);
		}
	}

}
