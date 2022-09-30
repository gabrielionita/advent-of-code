using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2021
{
    public class Day06 : DayBase<List<int>, long>
    {
        public override List<int> MapInput(string input)
        {
            return input.Split(',').Select(int.Parse).ToList();
        }

        public override long SolvePart1(List<int> input)
        {
            return Solve(GetFrequencies(input), 80);
        }

        public override long SolvePart2(List<int> input)
        {
            return Solve(GetFrequencies(input), 256);
        }

        private Dictionary<int, long> GetFrequencies(List<int> input)
        {
            var frequencies = new Dictionary<int, long>(9);
            var groups = input.GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());
            for (var i = 0; i <= 8; i++)
                frequencies.Add(i, groups.GetValueOrDefault(i, 0));
            return frequencies;
        }

        private long Solve(Dictionary<int, long> frequencies, int days)
        {
            for (var day = 1; day <= days; day++)
            {
                var temp = frequencies[0];

                for (var i = 0; i < frequencies.Count - 1; i++)
                    frequencies[i] = frequencies[i + 1];

                frequencies[6] += temp;
                frequencies[8] = temp;
            }

            return frequencies.Values.Sum();
        }

    }
}
