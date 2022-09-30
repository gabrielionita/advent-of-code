using System.Threading.Tasks;
using System.Threading;

namespace AdventOfCode.Abstractions
{
    public interface IInputStorage
    {
        public Task Write(int year, int day, string content, CancellationToken cancellationToken = default);

        public Task<string> Read(int year, int day, CancellationToken cancellationToken = default);
    }
}
