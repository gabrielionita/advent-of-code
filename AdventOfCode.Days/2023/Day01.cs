using System.Linq;

namespace AdventOfCode.Days2023
{
    public class Day01 : IDay<string[], int>
    {
        private readonly string[] numbersAsSrings = new[]
        {
             "one",
             "two",
             "three",
             "four",
             "five",
             "six",
             "seven",
             "eight",
             "nine"
        };

        public string[] MapInput(string input)
        {
            return input.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);
        }

        public int SolvePart1(string[] input)
        {
            var sum = 0;
            foreach(var line in input)
            {
                var firstNumber = line.First(c => char.IsDigit(c));
                var lastNumber = line.Last(c => char.IsDigit(c));
                var number = int.Parse($"{firstNumber}{lastNumber}");
                sum += number;
            }

            return sum;
        }

        public int SolvePart2(string[] input)
        {
            var sum = 0;
            foreach(var line in input)
            {
                var firstNumber = line.First(c => char.IsDigit(c));
                var firstNumberIndex = line.IndexOf(firstNumber);

                var firstNumberStringIndex = line.Length;
                var firstNumberString = 10;
                for(var i = 0; i < numbersAsSrings.Length; i++)
                {
                    var stringNumber = numbersAsSrings[i];
                    var index = line.IndexOf(stringNumber);
                    if(index != - 1 && index < firstNumberStringIndex)
                    {
                        firstNumberStringIndex = index;
                        firstNumberString = i + 1;
                    }
                }
               
                if(firstNumberIndex > firstNumberStringIndex)
                {
                    firstNumber = firstNumberString.ToString()[0];
                }


                var lastNumber = line.Last(c => char.IsDigit(c));
                var lastNumberIndex = line.LastIndexOf(lastNumber);

                var lastNumberStringIndex = -1;
                var lastNumberString = -1;
                for (var i = 0; i < numbersAsSrings.Length; i++)
                {
                    var stringNumber = numbersAsSrings[i];
                    var index = line.LastIndexOf(stringNumber);
                    if (index > lastNumberStringIndex)
                    {
                        lastNumberStringIndex = index;
                        lastNumberString = i + 1;
                    }
                }

                if (lastNumberString != -1 && lastNumberIndex < lastNumberStringIndex)
                {
                    lastNumber = lastNumberString.ToString().ToCharArray().First();
                }

                var number = int.Parse($"{firstNumber}{lastNumber}");
                sum += number;
            }

            return sum;
        }
    }
}
