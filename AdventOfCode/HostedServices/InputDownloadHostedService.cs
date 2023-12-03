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
			var year = configuration.GetValue("Year", DateTime.Now.Year);
			foreach (var day in Enumerable.Range(1, 25))
			{
				var content = await adventOfCodeClient.GetInput(year, day, cancellationToken);
				await inputStorage.Write(year, day, content, cancellationToken);
				await Task.Delay(100, cancellationToken);
			}
		}
	}
}
