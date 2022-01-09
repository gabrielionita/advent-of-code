using Microsoft.Extensions.DependencyInjection;
using System;

namespace AdventOfCode.Services
{
    public class DayFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DayFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public object Create(Type dayType)
        {
            return serviceProvider.GetRequiredService(dayType);
        }
    }
}