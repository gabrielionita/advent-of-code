using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddHttpClient<DayBase>(options =>
            {
                options.BaseAddress = new Uri("https://adventofcode.com/2020");
                options.DefaultRequestHeaders.Add("cookie", "session=53616c7465645f5f75863dbfc6d52e8f9f580c10217fb071065b5ff194739c0827a580f14cfee52456ea2e9c5a7e4fa3");
            });

            services.AddScoped<Day1>();
        }
    }
}
