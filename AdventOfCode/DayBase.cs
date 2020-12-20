using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public abstract class DayBase
    {
        private readonly HttpClient httpClient;
        private readonly int day;

        protected DayBase(HttpClient httpClient, int day)
        {
            this.httpClient = httpClient;
            this.day = day;
        }

        protected virtual async Task<string> GetInput()
        {
            return await httpClient.GetStringAsync($"day/{day}/input");
        }

        protected abstract Task<string> Solve(string input);

        private async Task Run()
        {
            var content = await GetInput();
            var solution = await Solve(content);
            Console.WriteLine($"Solution: {solution}");
        }
    }
}
