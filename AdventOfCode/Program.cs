using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            var startup = new Startup(null);
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
        }
    }
}
