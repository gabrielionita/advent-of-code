using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2024
{
    public class Day01 : IDay<(IEnumerable<int>, IEnumerable<int>), int>
    {
        public (IEnumerable<int>, IEnumerable<int>) MapInput(string input)
        {
            var list1 = new List<int>();
            var list2 = new List<int>();
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToArray();
            foreach (var line in lines)
            {
                var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                list1.Add(numbers[0]);
                list2.Add(numbers[1]);
            }

            return (list1, list2);  
        }

        public int SolvePart1((IEnumerable<int>, IEnumerable<int>) input)
        {
            var orderedZip = input.Item1.OrderBy(x => x).Zip(input.Item2.OrderBy(x => x));
            var sum = 0;
            foreach (var (first, second) in orderedZip)
            {
                var diff = Math.Abs(first - second);
                sum += diff;
            }

            return sum;
        }

        public int SolvePart2((IEnumerable<int>, IEnumerable<int>) input)
        {
            var similarity = 0;
            foreach(var item in input.Item1)
            {
                var count = input.Item2.Count(x => x == item);
                similarity += item * count;
            }

            return similarity;
        }
    }
}
