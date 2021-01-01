using AdventOfCode.Abstractions;
using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days2020
{
	public class Day12 : DayBase<string[], int>
	{
		private readonly Direction[] verticalDirections = new[] { Direction.Nord, Direction.South };
		private readonly Direction[] horizontalDirections = new[] { Direction.East, Direction.West };
		private readonly Direction[] negativeDirections = new[] { Direction.South, Direction.West };

		public Day12(HttpClient httpClient) : base(httpClient)
		{
		}

		public override string[] MapInput(string input)
		{
			return input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
		}

		public override int SolvePart1(string[] instructions)
		{
			var shipPosition = new Point(0, 0);
			var facing = Direction.East;
			foreach (var instruction in instructions)
			{
				var code = instruction[0..1];
				var value = int.Parse(instruction[1..]);

				if (code == "L" || code == "R")
				{
					value /= 90;
					if (code == "L")
					{
						value = 4 - value;
					}

					facing = (Direction)(((int)facing + value) % 4);
					continue;
				}
				else if (code == "F")
				{
					if (negativeDirections.Contains(facing))
					{
						value *= -1;
					}
					if (verticalDirections.Contains(facing))
					{
						shipPosition.Y += value;
					}
					else
					{
						shipPosition.X += value;
					}
					continue;
				}

				var direction = GetDirection(code);
				if (direction == Direction.Nord)
				{
					shipPosition.Y += value;
				}
				else if (direction == Direction.South)
				{
					shipPosition.Y -= value;
				}
				else if (direction == Direction.East)
				{
					shipPosition.X += value;
				}
				else if (direction == Direction.West)
				{
					shipPosition.X -= value;
				}
			}

			return Math.Abs(shipPosition.X) + Math.Abs(shipPosition.Y);
		}

		public override int SolvePart2(string[] instructions)
		{
			var shipPosition = new Point(0, 0);
			var waypoint = new Point(10, 1);
			foreach (var instruction in instructions)
			{
				var code = instruction[0..1];
				var value = int.Parse(instruction[1..]);

				if (code == "L" || code == "R")
				{
					if(code == "R")
					{
						value *= -1;
					}
					waypoint = RotatePoint(waypoint, value);
					continue;
				}
				else if (code == "F")
				{
					shipPosition.X += value * waypoint.X;
					shipPosition.Y += value * waypoint.Y;
					continue;
				}

				var direction = GetDirection(code);
				if (direction == Direction.Nord)
				{
					waypoint.Y += value;
				}
				else if (direction == Direction.South)
				{
					waypoint.Y -= value;
				}
				else if (direction == Direction.East)
				{
					waypoint.X += value;
				}
				else if (direction == Direction.West)
				{
					waypoint.X -= value;
				}
			}

			return Math.Abs(shipPosition.X) + Math.Abs(shipPosition.Y);
		}

		private Direction GetDirection(string code) => code switch
		{
			"N" => Direction.Nord,
			"E" => Direction.East,
			"S" => Direction.South,
			"W" => Direction.West,
			_ => throw new ArgumentException()
		};

		private Point RotatePoint(Point pointToRotate, double angleInDegrees)
		{
			double angleInRadians = angleInDegrees * (Math.PI / 180);
			double cosTheta = Math.Cos(angleInRadians);
			double sinTheta = Math.Sin(angleInRadians);
			return new Point
			{
				X = (int)Math.Round(cosTheta * pointToRotate.X - sinTheta * pointToRotate.Y, 0),
				Y = (int)Math.Round(sinTheta * pointToRotate.X + cosTheta * pointToRotate.Y, 0)
			};
		}
	}

	public enum Direction
	{
		Nord,
		East,
		South,
		West
	}
}
