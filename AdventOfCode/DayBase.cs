using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode
{
	public abstract class DayBase
    {
        private readonly HttpClient httpClient;
        private readonly int day;
        private string solution;

        protected DayBase(IHttpClientFactory httpClientFactory, int day)
        {
            httpClient = httpClientFactory.CreateClient("day-client");
            this.day = day;
        }

        protected virtual async Task<string> GetInput()
        {
            return await httpClient.GetStringAsync($"day/{day}/input");
        }

        protected abstract void SolvePart1(string input);

        protected abstract void SolvePart2(string input);

        public async Task Run()
        {
            var content = await GetInput();
            SolvePart1(content);
            if (string.IsNullOrEmpty(solution))
            {
                throw new SolutionNotFoundException();
            }
            Console.WriteLine($"Solution for part 1: {solution}");

            SolvePart2(content);
			if (string.IsNullOrEmpty(solution))
			{
                throw new SolutionNotFoundException();
			}
            Console.WriteLine($"Solution for part 2: {solution}");
        }
    }
}
