using AdventOfCode.Abstractions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Services
{
    public class AdventOfCodeClient : IAdventOfCodeClient
    {
        private readonly HttpClient httpClient;

        public AdventOfCodeClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetInput(int year, int day, CancellationToken cancellationToken)
        {
            var response = await httpClient.GetAsync($"{year}/day/{day}/input", cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}
