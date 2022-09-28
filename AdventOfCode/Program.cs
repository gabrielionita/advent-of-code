using AdventOfCode.Handlers;
using AdventOfCode.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			using var host = Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logging => logging.AddConsole().AddDebug())
				.ConfigureAppConfiguration(configuration => configuration.AddUserSecrets<Program>())
				.ConfigureServices((context, services) =>
				{
					services.AddSingleton<DayFactory>();
					services.AddSingleton<InputStorage>();
					services.AddSingleton<InputDownloadHandler>();
					services.AddSingleton<DayHandler>();
					services.AddHttpClient<AdventOfCodeClient>(options =>
					{
						options.BaseAddress = new Uri(context.Configuration["AdventOfCodeBaseUrl"]);
						options.DefaultRequestHeaders.Add("cookie", $"session={context.Configuration["CookieSession"]}");
					});

					var dayTypes = Assembly.Load("AdventOfCode.Days").ExportedTypes
						.Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface);

					foreach (var type in dayTypes)
					{
						services.AddTransient(type);
					}
				}).Build();

            if (args.Contains("--download=true"))
            {
				await host.Services.GetRequiredService<InputDownloadHandler>().DownloadInputs(CancellationToken.None);
            }

			await host.Services.GetRequiredService<DayHandler>().Execute(CancellationToken.None);
		}
	}
}
