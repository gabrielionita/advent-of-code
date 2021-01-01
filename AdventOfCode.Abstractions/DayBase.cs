namespace AdventOfCode
{
	public abstract class DayBase<TInput, TSolution>
	{
		private readonly int day;
		private readonly int year;

		protected DayBase()
		{
			var type = GetType();
			day = int.Parse(type.Name.Substring(3));
			year = int.Parse(type.FullName.Substring(type.FullName.IndexOf("Days") + 4, 4));
		}

		public string GetUrl() => $"{year}/day/{day}/input";

		public abstract TInput MapInput(string input);

		public abstract TSolution SolvePart1(TInput input);

		public abstract TSolution SolvePart2(TInput input);
	}
}
