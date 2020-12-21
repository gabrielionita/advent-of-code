using System;

namespace AdventOfCode.Days
{
	public class SolutionNotFoundException : Exception
	{
		private const string message = "No solution was found";
		public SolutionNotFoundException() : base(message)
		{

		}
	}
}
