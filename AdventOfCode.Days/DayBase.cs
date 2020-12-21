using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
	public abstract class DayBase 
	{
		private readonly HttpClient httpClient;
		private readonly int day;

		public object Solution { get; protected set; }

		protected DayBase(HttpClient httpClient)
		{
			this.httpClient = httpClient;
			day = int.Parse(GetType().Name[3..]);
		}

		public virtual async Task<string> GetInput()
		{
			var response = await httpClient.GetAsync($"day/{day}/input");
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}

		public abstract void SolvePart1(string input);

		public abstract void SolvePart2(string input);
	}
}
