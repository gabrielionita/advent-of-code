using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Abstractions
{
	public abstract class DayBase<TInput, TSolution>
    {
		private readonly int day;
        private readonly int year;
		private readonly HttpClient httpClient;

		protected DayBase(HttpClient httpClient)
		{
			this.httpClient = httpClient;
            var type = GetType();
			day = int.Parse(type.Name.Substring(3));
            year = int.Parse(type.FullName.Substring(type.FullName.IndexOf("Days") + 4, 4));
		}

        public async Task<string> GetStringContent()
		{
			var response = await httpClient.GetAsync($"{year}/day/{day}/input");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}

        public abstract TInput MapInput(string input);

        public abstract TSolution SolvePart1(TInput input);

        public abstract TSolution SolvePart2(TInput input);
	}
}
