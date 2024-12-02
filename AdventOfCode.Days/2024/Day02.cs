using System;
using System.Linq;

namespace AdventOfCode.Days2024
{
    public class Day02 : IDay<int[][], int>
    {
        public int[][] MapInput(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToArray();
            var matrix = new int[lines.Length][];
            for (var i = 0; i < lines.Length; i++)
            {
                var numbers = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                matrix[i] = numbers;
            }

            return matrix;
        }

        public int SolvePart1(int[][] input)
        {
            var safeReports = 0;
            for (var row = 0; row < input.Length; row++)
            {
                var safe = IsMonotonous(input[row]) && Diff(input[row]);
                if (safe)
                {
                    safeReports++;
                }
            }

            return safeReports;
        }

        public int SolvePart2(int[][] input)
        {
            var safeReports = 0;
            for (var row = 0; row < input.Length; row++)
            {
                if (IsMonotonous(input[row]) && Diff(input[row]))
                {
                    safeReports++;
                    continue;
                }

                for (var column = 0; column < input[row].Length; column++)
                {
                    var excludedColumn = input[row][0..column].Concat(input[row][(column + 1)..]).ToArray();
                    if (IsMonotonous(excludedColumn) && Diff(excludedColumn))
                    {
                        safeReports++;
                        break;
                    }
                }
            }

            return safeReports;
        }

        private bool IsMonotonous(int[] input)
        {
            var increasing = input[0] < input[1];
            for (var i = 1; i < input.Length - 1; i++)
            {
                if (increasing && input[i] > input[i + 1])
                {
                    return false;
                }
                else if (!increasing && input[i] < input[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private bool Diff(int[] input)
        {
            for (var i = 0; i < input.Length - 1; i++)
            {
                var diff = Math.Abs(input[i] - input[i + 1]);
                if (diff < 1 || diff > 3)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
