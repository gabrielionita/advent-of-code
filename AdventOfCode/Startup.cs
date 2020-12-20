using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("day-client", options =>
            {
                options.BaseAddress = new Uri(configuration["AdventOfCodeBaseUrl"]);
                options.DefaultRequestHeaders.Add("cookie", $"session={configuration["CookieSession"]}");
            });

            var dayClasses = typeof(DayBase).Assembly.ExportedTypes.Where(t => t.IsClass && t.IsPublic && !t.IsAbstract);

            foreach(var dayClass in dayClasses)
			{
                services.AddTransient(dayClass);
			}
        }
    }
}
