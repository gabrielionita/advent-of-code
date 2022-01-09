using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class AdventOfCodeClient
    {
        private readonly HttpClient httpClient;

        public AdventOfCodeClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetInput(int year, int day)
        {
            var response = await httpClient.GetAsync($"{year}/day/{day}/input");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
