using System;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day03 : IDay<char[][], int>
	{
		public char[][] MapInput(string input)
		{
			return input.Split('\n', System.StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToCharArray()).ToArray();
		}

		private int Convert(int[] bits)
		{
			var result = 0;
			for (var i = 0; i < bits.Length; i++)
			{
				if (bits[i] == 1)
					result += (int)Math.Pow(2, bits.Length - i - 1);
			}

			return result;
		}

		public int SolvePart1(char[][] input)
		{
			var columns = input[0].Length;
			var rows = input.Length;
			var gammaRate = new int[columns];
			var epsilonRate = new int[columns];
			for (var i = 0; i < columns; i++)
			{
				var oneCount = 0;
				for (var j = 0; j < rows; j++)
				{
					if (input[j][i] == '1')
						oneCount++;
				}

				gammaRate[i] = oneCount > rows / 2 ? 1 : 0;
				epsilonRate[i] = gammaRate[i] == 0 ? 1 : 0;
			}

			return Convert(gammaRate) * Convert(epsilonRate);

		}

		public int SolvePart2(char[][] input)
		{
			var inputClone = (char[][])input.Clone();
			var columns = inputClone[0].Length;
			for (var i = 0; i < columns; i++)
			{
				var rows = inputClone.Length;
				if (rows == 1)
					break;

				var oneCount = 0;
				for (var j = 0; j < rows; j++)
				{
					if (inputClone[j][i] == '1')
						oneCount++;
				}

				var bitToKeep = oneCount < rows - oneCount ? '0' : '1';
				inputClone = inputClone.Where(c => c[i] == bitToKeep).ToArray();
			}
			var oxygenGeneratorRating = Convert(inputClone[0].Select(c => int.Parse(c.ToString())).ToArray());

			inputClone = (char[][])input.Clone();
			for (var i = 0; i < columns; i++)
			{
				var rows = inputClone.Length;
				if (rows == 1)
					break;

				var oneCount = 0;
				for (var j = 0; j < rows; j++)
				{
					if (inputClone[j][i] == '1')
						oneCount++;
				}

				var bitToKeep = oneCount >= rows - oneCount ? '0' : '1';
				inputClone = inputClone.Where(c => c[i] == bitToKeep).ToArray();
			}

			var co2ScrubberRating = Convert(inputClone[0].Select(c => int.Parse(c.ToString())).ToArray());

			return oxygenGeneratorRating * co2ScrubberRating;
		}
	}
}
