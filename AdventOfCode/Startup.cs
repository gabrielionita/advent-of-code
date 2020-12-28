using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;

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

			services.AddLogging(c => c.AddConsole().AddDebug());

			foreach (var type in GetDayTypes())
			{
				services.AddTransient(type);
			}
		}

		private IEnumerable<Type> GetDayTypes()
		{
			return Assembly.Load("AdventOfCode.Days").ExportedTypes
				.Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface);
		}
	}
}
