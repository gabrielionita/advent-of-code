namespace AdventOfCode
{
    public interface IDay<TInput, TSolution>
    {
        public TInput MapInput(string input);
        public TSolution SolvePart1(TInput input);
        public TSolution SolvePart2(TInput input);
    }
}
