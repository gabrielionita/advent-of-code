using System;
using System.Net.Http;

namespace AdventOfCode.Days
{
	public class Day05 : DayBase
	{
		public Day05(HttpClient httpClient) : base(httpClient)
		{
		}

		public string[] InitData(string input)
		{
			return input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
		}

		private int GetRow(string code)
		{
			var rowMin = 0;
			var rowMax = 127;
			foreach (var symbol in code.Substring(0, 7))
			{
				if (symbol == 'F')
				{
					rowMax = (rowMax + rowMin - 1) / 2;
				}
				else
				{
					rowMin = (rowMax + rowMin + 1) / 2;
				}
			}

			return rowMin;
		}

		private int GetColumn(string code)
		{
			var colMin = 0;
			var colMax = 7;
			foreach (var symbol in code.Substring(7))
			{
				if (symbol == 'R')
				{
					colMin = (colMax + colMin + 1) / 2;
				}
				else
				{
					colMax = (colMax + colMin - 1) / 2;
				}
			}

			return colMin;
		}

		public override void SolvePart1(string input)
		{
			var codes = InitData(input);
			var maxSeatId = 0;
			foreach (var code in codes)
			{
				var row = GetRow(code);
				var column = GetColumn(code);
				var seatId = row * 8 + column;
				if (maxSeatId < seatId)
				{
					maxSeatId = seatId;
				}
			}

			Solution = maxSeatId;
		}

		public override void SolvePart2(string input)
		{
			var codes = InitData(input);
			var ids = new int[128, 8];
			foreach (var code in codes)
			{
				var row = GetRow(code);
				var column = GetColumn(code);

				ids[row, column] = row * 8 + column;
			}

			var currentId = 0;
			var previousId = 0;
			foreach (var nextId in ids)
			{
				if(previousId != 0 && currentId == 0 && nextId != 0)
				{
					currentId = (previousId + nextId) / 2;
					Solution = currentId;
					break;
				}
				previousId = currentId;
				currentId = nextId;
			}
		}
	}
}
