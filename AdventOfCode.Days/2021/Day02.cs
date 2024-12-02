using System;
using System.Linq;

namespace AdventOfCode.Days2021
{
	public class Day02 : IDay<DirectionValue[], int>
	{
		public DirectionValue[] MapInput(string input)
		{
			return input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(c => new DirectionValue(c)).ToArray();
		}

		public int SolvePart1(DirectionValue[] input)
		{
			var horizontalPosition = 0;
			var depth = 0;

			foreach (var item in input)
			{
				if (item.Direction == Direction.Forward)
					horizontalPosition += item.Value;
				else if (item.Direction == Direction.Down)
					depth += item.Value;
				else
					depth -= item.Value;
			}

			return horizontalPosition * depth;
		}

		public int SolvePart2(DirectionValue[] input)
		{
			var horizontalPosition = 0;
			var depth = 0;
			var aim = 0;

			foreach (var item in input)
			{
				if (item.Direction == Direction.Down)
					aim += item.Value;
				else if (item.Direction == Direction.Up)
					aim -= item.Value;
				else
				{
					horizontalPosition += item.Value;
					depth += aim * item.Value;
				}
			}

			return horizontalPosition * depth;
		}
	}

	public class DirectionValue
	{
		public Direction Direction { get; set; }
		public int Value { get; set; }

		public DirectionValue(string line)
		{
			var split = line.Split(' ');
			Direction = Enum.Parse<Direction>(split[0], true);
			Value = int.Parse(split[1]);
		}
	}

	public enum Direction
	{
		Forward,
		Down,
		Up
	}
}
