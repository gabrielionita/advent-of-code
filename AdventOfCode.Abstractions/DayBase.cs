namespace AdventOfCode
{
	public abstract class DayBase<TInput, TSolution>
	{
		public abstract TInput MapInput(string input);
		public abstract TSolution SolvePart1(TInput input);
		public abstract TSolution SolvePart2(TInput input);
	}
}
