using AdventOfCode.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days2020
{
    public class Day10 : DayBase<int[], int>
    {
        public Day10(HttpClient httpClient, ILogger<Day10> logger) : base(httpClient, logger)
        {
        }

        protected override int[] MapInput(string input)
        {
            return input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(line => int.Parse(line)).ToArray();
        }

        protected override int SolvePart1(int[] jolts)
        {
            var difference1Jolt = 0;
            var difference2Jolt = 0;
            var difference3Jolt = 1;
            jolts = jolts.OrderBy(jolt => jolt).ToArray();
            var currentJolt = 0;
            foreach(var jolt in jolts)
            {
                var difference = jolt - currentJolt;
                if(difference == 1)
                {
                    difference1Jolt++;
                }
                else if(difference == 2)
                {
                    difference2Jolt++;
                }
                else if(difference == 3)
                {
                    difference3Jolt++;
                }
                currentJolt = jolt;
            }

            return difference1Jolt * difference3Jolt;
        }

        protected override int SolvePart2(int[] jolts)
        {
            var list = jolts.OrderByDescending(jolt => jolt).ToList();
            list.Insert(0, list[0] + 3);
            list.Add(0);
            jolts = list.ToArray();
            

            return CalculateArrangements(jolts);
        }

        private int CalculateArrangements(int[] jolts)
        {

            if(jolts.Length > 2)
            {
                return 1;
            }

            var arrangements = 0;
            for(var i = 0; i < jolts.Length; i++)
            {
                arrangements += CalculateArrangements(jolts[(i+1)..]);
            }


            return arrangements;
        }
    }
}
