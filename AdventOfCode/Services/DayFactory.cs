using AdventOfCode.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Services
{
    public class DayFactory : IDayFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DayFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public object Create(int? year, int? day)
        {
            var dayType = GetDayType(year, day);
            return serviceProvider.GetRequiredService(dayType);
        }

        private Type GetDayType(int? year, int? day)
        {
            if (year.HasValue && day.HasValue)
            {
                return Type.GetType($"AdventOfCode.Days{year}.Day{day:00}, AdventOfCode.Days", true);
            }

            return typeof(Days2020.Day01).Assembly.ExportedTypes
                .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface && Regex.IsMatch(type.Name, "^Day(\\d{2,})$"))
                .OrderByDescending(type => type.FullName)
                .FirstOrDefault();
        }
    }
}