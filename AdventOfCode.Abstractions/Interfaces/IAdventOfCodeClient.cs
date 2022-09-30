using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Interfaces
{
    public interface IAdventOfCodeClient
    {
        public Task<string> GetInput(int year, int day, CancellationToken cancellationToken = default);
    }
}
