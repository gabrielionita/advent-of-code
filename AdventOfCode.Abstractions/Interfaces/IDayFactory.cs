namespace AdventOfCode.Interfaces
{
    public interface IDayFactory
    {
        object Create(int? year, int? day);
    }
}
