using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day09 : DayBase<int[,], int>
	{
		public override int[,] MapInput(string input)
		{
			var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToArray();
			var length = lines[0].Length;
			var array = new int[lines.Length, length];
			for (var i = 0; i < array.GetLength(0); i++)
				for (var j = 0; j < array.GetLength(1); j++)
					array[i, j] = int.Parse(lines[i][j].ToString());

			return array;
		}

		private int[,] GetLowPoints(int[,] input)
		{
			var lowPoints = new int[input.GetLength(0), input.GetLength(1)];
			for (var i = 0; i < lowPoints.GetLength(0); i++)
			{
				for(var j = 0; j < lowPoints.GetLength(1); j++)
				{
					var window = new List<int>();
					if (i > 0)
						window.Add(input[i - 1, j]);
					if (i < input.GetLength(0) - 1)
						window.Add(input[i + 1, j]);
					if (j > 0)
						window.Add(input[i, j - 1]);
					if (j < input.GetLength(1) - 1)
						window.Add(input[i, j + 1]);

					window.Add(input[i, j]);
					if (window.Min() == input[i, j] && window.Count(w => w == input[i, j]) == 1)
						lowPoints[i, j] = input[i, j];
					else
						lowPoints[i, j] = -1;
				}
			}

			return lowPoints;
		}

		public override int SolvePart1(int[,] input)
		{
			var lowPoints = GetLowPoints(input);
			var flattenLowPoints = lowPoints.Cast<int>();
			return flattenLowPoints.Sum(p => p + 1);
		}

		public override int SolvePart2(int[,] input)
		{
			var lowPoints = GetLowPoints(input);
			var basinSizes = new List<int>(lowPoints.Cast<int>().Count(p => p > 0));
			for(var i = 0; i < lowPoints.GetLength(0); i++)
			{
				for(var j = 0; j < lowPoints.GetLength(1); j++)
				{
					if (lowPoints[i, j] == -1)
						continue;

					var basinSize = GetBasinSize(input, i, j);
					basinSizes.Add(basinSize);
				}
			}

			return basinSizes.OrderByDescending(s => s).Take(3).Aggregate(1, (s1, s2) => s1 * s2);
		}

		private int GetBasinSize(int[,] input, int i, int j)
		{
			//TODO: sare peste 9 in alta zona
			var sum = input[i, j];
			input[i, j] = -1;
			for(var i1 = -1; i1 <= 1; i1++)
			{
				for(var j1 = -1; j1 <= 1; j1++)
				{
					var iIndex = i + i1;
					var jIndex = j + j1;
					if (iIndex < 0 || iIndex >= input.GetLength(0) || jIndex < 0 || jIndex >= input.GetLength(1))
						continue;

					if (input[iIndex, jIndex] == 9 || input[iIndex, jIndex] == -1)
						continue;

					sum += GetBasinSize(input, iIndex, jIndex);
				}
			}

			return sum;
		}
	}
}
