using AdventOfCode.HostedServices;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDays(this IServiceCollection services)
		{
			var dayTypes = typeof(AdventOfCode.Days2020.Day01).Assembly.ExportedTypes
					.Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface && Regex.IsMatch(type.Name, @"^Day(\d{2,})$"));

			foreach (var type in dayTypes)
				services.AddTransient(type);

			return services;
		}

		public static IServiceCollection AddHostedService(this IServiceCollection services, bool download)
		{
			if (download)
			{
				var current = Directory.GetCurrentDirectory();
				if (current.Contains("bin"))
				{
					Directory.SetCurrentDirectory(current.Substring(0, current.IndexOf("bin")));
				}
				services.AddHostedService<InputDownloadHostedService>();
			}
			else
				services.AddHostedService<DayHostedService>();

			return services;
		}
	}
}
