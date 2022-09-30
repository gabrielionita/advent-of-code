using AdventOfCode.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.HostedServices
{
    public class InputDownloadHostedService : BackgroundService
    {
        private readonly IConfiguration configuration;
        private readonly IInputStorage inputStorage;
        private readonly IAdventOfCodeClient adventOfCodeClient;

        public InputDownloadHostedService(IConfiguration configuration, IInputStorage inputStorage, IAdventOfCodeClient adventOfCodeClient)
        {
            this.configuration = configuration;
            this.inputStorage = inputStorage;
            this.adventOfCodeClient = adventOfCodeClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var year = configuration.GetValue<int?>("Year");
            if (year == null)
            {
                throw new ArgumentNullException(nameof(year), "Cannot download inputs without an year specified");
            }

            foreach (var day in Enumerable.Range(1, 25))
            {
                var content = await adventOfCodeClient.GetInput(year.Value, day, cancellationToken);
                await inputStorage.Write(year.Value, day, content, cancellationToken);
                await Task.Delay(100, cancellationToken);
            }
        }
    }
}
