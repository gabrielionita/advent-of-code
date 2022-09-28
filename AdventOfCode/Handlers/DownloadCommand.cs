using AdventOfCode.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Handlers
{
    [Command("download")]
    public class DownloadCommand
    {
        private readonly InputStorage inputStorage;
        private readonly AdventOfCodeClient adventOfCodeClient;

        [Option]
        [Required]
        public int? Year { get; set; }

        public DownloadCommand(InputStorage inputStorage, AdventOfCodeClient adventOfCodeClient)
        {
            this.inputStorage = inputStorage;
            this.adventOfCodeClient = adventOfCodeClient;
        }

        public async Task OnExecuteAsync(CancellationToken cancellationToken)
        {
            if (Year == null)
            {
                throw new ArgumentNullException(nameof(Year), "Cannot download inputs without an year specified");
            }

            foreach (var day in Enumerable.Range(1, 25))
            {
                var content = await adventOfCodeClient.GetInput(Year.Value, day, cancellationToken);
                await inputStorage.Write(Year.Value, day, content, cancellationToken);
                await Task.Delay(100, cancellationToken);
            }
        }
    }
}
