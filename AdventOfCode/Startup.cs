using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using AdventOfCode.Days;

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
            services.AddHttpClient(Options.DefaultName, options =>
            {
                options.BaseAddress = new Uri(configuration["AdventOfCodeBaseUrl"]);
                options.DefaultRequestHeaders.Add("cookie", $"session={configuration["CookieSession"]}");
            });

            var dayTypes = typeof(DayBase).Assembly.ExportedTypes.Where(t => t.IsClass && t.IsPublic && !t.IsAbstract);

            foreach(var type in dayTypes)
			{
                services.AddTransient(type);
			}
        }
    }
}
