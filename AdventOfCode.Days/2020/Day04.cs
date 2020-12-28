using AdventOfCode.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days2020
{
	public class Day04 : DayBase<IEnumerable<Dictionary<string, string>>, int>
	{
		private readonly string[] mandatoryKeys = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
		private readonly string[] eyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

		public Day04(HttpClient httpClient) : base(httpClient)
		{
		}

        public override IEnumerable<Dictionary<string, string>> MapInput(string input)
		{
			return input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
				.Select(c => c.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
						.ToDictionary(key => key.Split(":")[0], value => value.Split(":")[1]));
		}

        public override int SolvePart1(IEnumerable<Dictionary<string, string>> passports)
		{
			var validPassports = 0;
			foreach (var passport in passports)
			{

				if (mandatoryKeys.All(key => passport.ContainsKey(key)))
				{
					validPassports++;
				}

			}
			return validPassports;
		}

        public override int SolvePart2(IEnumerable<Dictionary<string, string>> passports)
		{
			var validPassports = 0;
			foreach (var passport in passports)
			{
				if (mandatoryKeys.All(key => passport.ContainsKey(key)))
				{
					if (!int.TryParse(passport["byr"], out var birthYear) || (birthYear < 1920 || birthYear > 2002))
					{
						continue;
					}
					if (!int.TryParse(passport["iyr"], out var issueYear) || (issueYear < 2010 || issueYear > 2020))
					{
						continue;
					}
					if (!int.TryParse(passport["eyr"], out var expirationYear) || (expirationYear < 2020 || expirationYear > 2030))
					{
						continue;
					}

					if (!passport["hcl"].StartsWith("#") || !int.TryParse(passport["hcl"].Substring(1), System.Globalization.NumberStyles.HexNumber, null, out var hairColor))
					{
						continue;
					}

					if (!passport["hgt"].EndsWith("cm") && !passport["hgt"].EndsWith("in"))
					{
						continue;
					}

					var indexOfHeightUnit = passport["hgt"].IndexOf("cm");
					if (indexOfHeightUnit < 0)
					{
						indexOfHeightUnit = passport["hgt"].IndexOf("in");
					}

					if (!int.TryParse(passport["hgt"].Substring(0, indexOfHeightUnit), out var height))
					{
						continue;
					}

					if (passport["hgt"].EndsWith("cm") && (height < 150 || height > 193))
					{
						continue;
					}

					if (passport["hgt"].EndsWith("in") && (height < 59 || height > 76))
					{
						continue;
					}


					if (!eyeColors.Any(color => color == passport["ecl"]))
					{
						continue;
					}
					if (!int.TryParse(passport["pid"], out var pid) || passport["pid"].Length != 9)
					{
						continue;
					}

					validPassports++;
				}

			}
			return validPassports;
		}
	}
}
