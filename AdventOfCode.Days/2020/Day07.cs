using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2020
{
	public class Day07 : DayBase<Dictionary<string, Dictionary<string, int>>, int>
	{
		private const string shinyGoldBagName = "shiny gold";

		public override Dictionary<string, Dictionary<string, int>> MapInput(string input)
		{
			return input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
				.Where(line => !line.Contains("no other bags"))
				.OrderByDescending(c => c.Contains(shinyGoldBagName))
				.ToDictionary(line => line.Substring(0, line.IndexOf(" bags")),
							line => line.Substring(line.IndexOf("contain") + "contain ".Length)
							.Split(", ", StringSplitOptions.RemoveEmptyEntries)
							.ToDictionary(l => l.Substring(l.IndexOf(" ") + 1, l.LastIndexOf(" ") - l.IndexOf(" ") - 1),
										l => l.Contains("no other bags") ? 0 : int.Parse(l.Substring(0, l.IndexOf(" ")))));

		}

		public override int SolvePart1(Dictionary<string, Dictionary<string, int>> bags)
		{
			var bagsThatAlreadyContainShinyGold = new List<string>();
			var oldAnswer = 0;
			var answer = 0;
			do
			{
				oldAnswer = answer;
				answer = 0;
				foreach (var bag in bags.Keys)
				{
					if (bagsThatAlreadyContainShinyGold.Contains(bag))
					{
						answer++;
					}
					else if (bags[bag].ContainsKey(shinyGoldBagName))
					{
						answer++;
						bagsThatAlreadyContainShinyGold.Add(bag);
					}
					else if (bags[bag].Any(b => bagsThatAlreadyContainShinyGold.Contains(b.Key)))
					{
						answer++;
						bagsThatAlreadyContainShinyGold.Add(bag);
					}
				}
			}
			while (answer != oldAnswer);

			return answer;
		}

		public override int SolvePart2(Dictionary<string, Dictionary<string, int>> bags)
		{
			return CountRequiredBagsInsideOf(shinyGoldBagName, bags);
		}

		private int CountRequiredBagsInsideOf(string bagName, Dictionary<string, Dictionary<string, int>> bags)
		{
			var answer = 0;
			if (!bags.ContainsKey(bagName))
			{
				return 0;
			}

			foreach (var bag in bags[bagName])
			{
				answer += bag.Value + bag.Value * CountRequiredBagsInsideOf(bag.Key, bags);
			}

			return answer;
		}
	}
}
