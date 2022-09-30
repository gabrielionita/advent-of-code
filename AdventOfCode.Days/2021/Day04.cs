using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day04 : DayBase<DataDay04, int>
	{
		public override DataDay04 MapInput(string input)
		{
			var lines = input.Split('\n');
			var data = new DataDay04
			{
				Numbers = lines[0].Split(',').Select(int.Parse).ToArray()
			};
			var boards = new List<int[][]>();
			for (var i = 2; i < lines.Length; i++)
			{
				var board = new int[5][];
				if (string.IsNullOrEmpty(lines[i]))
				{
					continue;
				}

				for (var j = 0; j < 5; j++)
				{
					board[j] = lines[i + j].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
				}
				i += 5;
				boards.Add(board);
			}
			data.Boards = boards.ToArray();
			return data;
		}


		private void MarkBoard(int[][] board, int number)
		{
			for (var i = 0; i < board.Length; i++)
				for (var j = 0; j < board[i].Length; j++)
					if (board[i][j] == number)
						board[i][j] = -1;
		}

		private bool AnyRowIsMarked(int[][] board)
		{
			var marked = true;
			for (var i = 0; i < board.Length; i++)
			{
				marked = true;
				for (var j = 0; j < board[i].Length; j++)
					if (board[i][j] != -1)
					{
						marked = false;
						break;
					}


				if (marked)
					break;
			}
			return marked;
		}

		private bool AnyColumnIsMarked(int[][] board)
		{
			var marked = true;
			for (var i = 0; i < board.Length; i++)
			{
				marked = true;
				for (var j = 0; j < board[i].Length; j++)
					if (board[j][i] != -1)
					{
						marked = false;
						break;
					}


				if (marked)
					break;
			}
			return marked;
		}

		private int CalculateScore(int[][] board, int number)
		{
			var sum = 0;
			for (var i = 0; i < board.Length; i++)
				for (var j = 0; j < board[i].Length; j++)
					if (board[i][j] != -1)
						sum += board[i][j];

			return sum * number;
		}

		public override int SolvePart1(DataDay04 input)
		{
			foreach (var number in input.Numbers)
			{
				foreach (var board in input.Boards)
				{
					MarkBoard(board, number);

					if (AnyRowIsMarked(board) || AnyColumnIsMarked(board))
					{
						return CalculateScore(board, number);
					}
				}
			}

			return 0;
		}

		public override int SolvePart2(DataDay04 input)
		{
			var winningBoards = new List<int>();
			foreach (var number in input.Numbers)
			{
				for (var boardIndex = 0; boardIndex < input.Boards.Length; boardIndex++)
				{
					if (winningBoards.Contains(boardIndex))
						continue;
					var board = input.Boards[boardIndex];

					MarkBoard(board, number);

					if (AnyRowIsMarked(board) || AnyColumnIsMarked(board))
					{
						winningBoards.Add(boardIndex);
						if (winningBoards.Count == input.Boards.Length)
							return CalculateScore(board, number);
					}
				}
			}

			return 0;
		}
	}

	public class DataDay04
	{
		public int[] Numbers { get; set; }
		public int[][][] Boards { get; set; }
	}
}
