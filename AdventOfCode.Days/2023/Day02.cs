using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2023
{
    public class Day02 : DayBase<Dictionary<int, Dictionary<string, int>>, long>
    {
        public override Dictionary<int, Dictionary<string, int>> MapInput(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var result = new Dictionary<int, Dictionary<string, int>>();
            foreach (var line in lines)
            {
                var indexOfFirstSpace = line.IndexOf(" ");
                var indexOfFirstColon = line.IndexOf(":");
                var game = int.Parse(line.Substring(indexOfFirstSpace, indexOfFirstColon - indexOfFirstSpace));

                var dictionary = new Dictionary<string, int>();
                var sets = line.Substring(indexOfFirstColon + 1).Split(";");
                foreach (var set in sets)
                {
                    var groups = set.Split(',');
                    foreach (var group in groups)
                    {
                        var items = group.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        var number = int.Parse(items[0]);
                        var color = items[1];
                        if (dictionary.ContainsKey(color) &&  dictionary[color] < number || !dictionary.ContainsKey(color))
                        {
                            dictionary[color] = number;
                        }
                    }

                }

                result.Add(game, dictionary);
            }

            return result;
        }

        public override long SolvePart1(Dictionary<int, Dictionary<string, int>> games)
        {
            var dictionary = new Dictionary<string, int>
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 },
            };

            var sum = 0;
            foreach(var game in games)
            {
                var possible = true;
                foreach(var cube in dictionary)
                {
                    if (game.Value[cube.Key] > cube.Value)
                    {
                        possible = false; 
                        break;
                    }
                }
                if (possible)
                {
                    sum += game.Key;
                }
            }
            return sum;
        }

        public override long SolvePart2(Dictionary<int, Dictionary<string, int>> games)
        {
            var sum = 0;
            foreach(var game in games)
            {
                var power = game.Value.Values.Aggregate(1, (a, b) => a * b);
                sum += power;
            }

            return sum;
        }
    }
}
