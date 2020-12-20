using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
	public abstract class DayBase
    {
        private readonly HttpClient httpClient;
        private readonly int day;
        protected string solution;

        protected DayBase(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("day-client");
            day = int.Parse(GetType().Name[3..]);
        }

        protected virtual async Task<string> GetInput()
        {

            var response = await httpClient.GetAsync($"day/{day}/input");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
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
