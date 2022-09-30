using AdventOfCode.Interfaces;
using AdventOfCode.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.AddConsole().AddDebug())
                .ConfigureAppConfiguration(configuration => configuration.AddUserSecrets<Program>())
                .ConfigureServices((context, services) =>
                {
                    services.AddDays();
                    services.AddHostedService(args.Contains("--download=true"));
                    services.AddSingleton<IDayFactory, DayFactory>();
                    services.AddSingleton<IInputStorage, InputStorage>();
                    services.AddHttpClient<IAdventOfCodeClient, AdventOfCodeClient>(options =>
                    {
                        options.BaseAddress = new Uri(context.Configuration["AdventOfCode:BaseUrl"]);
                        options.DefaultRequestHeaders.Add("cookie", $"session={context.Configuration["AdventOfCode:CookieSession"]}");
                    });

                }).RunConsoleAsync();
        }
    }
}
