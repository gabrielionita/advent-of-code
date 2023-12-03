using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2023
{
    public class Day03 : DayBase<char[,], int>
    {
        public override char[,] MapInput(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToArray();
            var schematic = new char[lines.Length, lines[0].Length];
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[i].Length; j++)
                {
                    schematic[i, j] = lines[i][j];
                }
            }

            return schematic;
        }

        public override int SolvePart1(char[,] schematic)
        {
            var sum = 0;
            for (var i = 0; i < schematic.GetLength(0); i++)
            {
                var numberStartIndex = -1;
                var numberEndIndex = -1;
                for (var j = 0; j < schematic.GetLength(1); j++)
                {
                    if (char.IsDigit(schematic[i, j]))
                    {
                        if (numberStartIndex == -1)
                        {
                            numberStartIndex = j;
                            numberEndIndex = -1;
                        }
                    }
                    else if (numberStartIndex != -1)
                    {
                        numberEndIndex = j - 1;
                    }

                    if (numberStartIndex != -1 && numberEndIndex == -1 && j == schematic.GetLength(1) - 1)
                    {
                        numberEndIndex = j;
                    }

                    if (numberStartIndex != -1 && numberEndIndex != -1)
                    {
                        var number = 0;
                        for (var k = numberStartIndex; k <= numberEndIndex; k++)
                        {
                            number = number * 10 + int.Parse(schematic[i, k].ToString());
                        }

                        if (NumberIsAdjacentToSymbol(schematic, numberStartIndex, numberEndIndex, i))
                        {
                            sum += number;
                        }

                        numberStartIndex = -1;
                        numberEndIndex = -1;
                    }
                }
            }

            return sum;
        }

        private static bool NumberIsAdjacentToSymbol(char[,] schematic, int numberStartIndex, int numberEndIndex, int i)
        {
            for (var k = i - 1; k < i + 2; k++)
            {
                for (var l = numberStartIndex - 1; l <= numberEndIndex + 1; l++)
                {
                    if (k < 0 || k >= schematic.GetLength(0))
                    {
                        break;
                    }

                    if (l < 0 || l >= schematic.GetLength(1))
                    {
                        continue;
                    }

                    if (k == i && l >= numberStartIndex && l <= numberEndIndex)
                    {
                        continue;
                    }

                    if (schematic[k, l] != '.')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override int SolvePart2(char[,] schematic)
        {
            var sum = 0;
            for (var i = 0; i < schematic.GetLength(0); i++)
            {
                for (var j = 0; j < schematic.GetLength(1); j++)
                {
                    if (schematic[i, j] != '*')
                    {
                        continue;
                    }

                    sum += GearRatio(schematic, i, j);
                }
            }

            return sum;
        }

        private int GearRatio(char[,] schematic, int i, int j)
        {
            var adjacentNumbers = new HashSet<int>();
            for (var k = i - 1; k < i + 2; k++)
            {
                for (var l = j - 1; l < j + 2; l++)
                {
                    if (k < 0 || k >= schematic.GetLength(0))
                    {
                        break;
                    }

                    if (l < 0 || l >= schematic.GetLength(1))
                    {
                        continue;
                    }

                    if (char.IsDigit(schematic[k, l]))
                    {
                        var number = FindNumber(schematic, k, l);
                        adjacentNumbers.Add(number);
                    }
                }
            }

            return adjacentNumbers.Count == 2 ? adjacentNumbers.Aggregate(1, (a, b) => a * b) : 0;
        }

        private int FindNumber(char[,] schematic, int i, int startingPosition)
        {
            var j = startingPosition - 1;
            var numberStartIndex = startingPosition;
            var numberEndIndex = startingPosition;
            while (j >= 0)
            {
                if (!char.IsDigit(schematic[i, j]))
                    break;

                numberStartIndex = j;
                j--;
            }

            j = startingPosition + 1;
            while (j < schematic.GetLength(1))
            {
                if (!char.IsDigit(schematic[i, j]))
                    break;

                numberEndIndex = j;
                j++;
            }

            var number = 0;
            for (var k = numberStartIndex; k <= numberEndIndex; k++)
            {
                number = number * 10 + int.Parse(schematic[i, k].ToString());
            }

            return number;
        }
    }
}
