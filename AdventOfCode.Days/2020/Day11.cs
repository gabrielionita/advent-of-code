using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode.Days2020
{
	public class Day11 : IDay<char[,], int>
	{
		private const char EmptrySeat = 'L';
		private const char OccupiedSeat = '#';
		private const char Floor = '.';

		public char[,] MapInput(string input)
		{
			var jagged = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToArray()).ToArray();
			var array = new char[jagged.Length, jagged[0].Length];
			for (var i = 0; i < jagged.Length; i++)
			{
				for (var j = 0; j < jagged[i].Length; j++)
				{
					array[i, j] = jagged[i][j];
				}
			}
			return array;
		}

		public int SolvePart1(char[,] map)
		{
			ChangeSeats(map, 4, false);
			return map.Cast<char>().Count(seat => seat == OccupiedSeat);
		}

		public int SolvePart2(char[,] map)
		{
			ChangeSeats(map, 5, true);
			return map.Cast<char>().Count(seat => seat == OccupiedSeat);
		}

		private char[] GetAdjacentSeats(char[,] map, int row, int column, bool firstSeat)
		{
			var adjacent = new List<char>();
			var rows = map.GetLength(0);
			var columns = map.GetLength(1);

			for (var i = -1; i <= 1; i++)
			{
				for (var j = -1; j <= 1; j++)
				{
					if (i == 0 && j == 0)
					{
						continue;
					}
					var currentRow = row + i;
					var currentColumn = column + j;
					if (firstSeat)
					{
						while (currentRow >= 0 && currentRow < rows && currentColumn >= 0 && currentColumn < columns)
						{
							if (map[currentRow, currentColumn] != Floor)
							{
								adjacent.Add(map[currentRow, currentColumn]);
								break;
							}
							currentRow += i;
							currentColumn += j;
						}
					}
					else
					{
						if (currentRow < 0 || currentColumn < 0 || currentRow >= rows || currentColumn >= columns)
						{
							continue;
						}
						if (map[currentRow, currentColumn] != Floor)
						{
							adjacent.Add(map[currentRow, currentColumn]);
						}
					}
				}
			}

			return adjacent.ToArray();
		}

		private void ChangeSeats(char[,] map, int maxOccupiedSeats, bool firstSeat)
		{
			var changeDetected = false;
			var oldMap = map.Clone() as char[,];
			for (var i = 0; i < oldMap.GetLength(0); i++)
			{
				for (var j = 0; j < oldMap.GetLength(1); j++)
				{
					if (oldMap[i, j] == Floor)
					{
						continue;
					}

					var adjacentSeats = GetAdjacentSeats(oldMap, i, j, firstSeat);
					var occupiedSeats = adjacentSeats.Count(seat => seat == OccupiedSeat);
					if (oldMap[i, j] == EmptrySeat && occupiedSeats == 0)
					{
						map[i, j] = OccupiedSeat;
						changeDetected = true;
					}
					else if (oldMap[i, j] == OccupiedSeat && occupiedSeats >= maxOccupiedSeats)
					{
						map[i, j] = EmptrySeat;
						changeDetected = true;
					}
				}
			}
			if (changeDetected)
			{
				ChangeSeats(map, maxOccupiedSeats, firstSeat);
			}
		}
	}
}
