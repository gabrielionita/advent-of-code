using System;

namespace AdventOfCode.Abstractions
{
    public interface IDayFactory
    {
        object Create(int? year, int? day);
    }
}
