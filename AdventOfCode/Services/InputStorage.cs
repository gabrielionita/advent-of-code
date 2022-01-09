using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Services
{
    public class InputStorage
    {
        public async Task Write(int year, int day, string content, CancellationToken cancellationToken = default)
        {
            await File.WriteAllTextAsync($"Inputs/{year}/Day{day:00}.txt", content, cancellationToken);
        }

        public async Task<string> Read(int year, int day, CancellationToken cancellationToken)
        {
            return await File.ReadAllTextAsync($"Inputs/{year}/Day{day:00}.txt", cancellationToken);
        }
    }
}
