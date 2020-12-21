using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using AdventOfCode.Days;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AdventOfCode
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpClient(Options.DefaultName, options =>
			{
				options.BaseAddress = new Uri(configuration["AdventOfCodeBaseUrl"]);
				options.DefaultRequestHeaders.Add("cookie", $"session={configuration["CookieSession"]}");
			});

			services.AddLogging(c => c.AddConsole());

			foreach (var type in GetDayTypes())
			{
				services.AddTransient(type);
			}
		}

		private static IEnumerable<Type> GetDayTypes()
		{
			return typeof(DayBase).Assembly.ExportedTypes.Where(t => t.IsSubclassOf(typeof(DayBase)));
		}
	}
}
