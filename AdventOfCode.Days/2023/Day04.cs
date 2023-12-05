using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2023
{
    public class Day04 : DayBase<string[], int>
    {
        public override string[] MapInput(string input)
        {
            return input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        public override int SolvePart1(string[] cards)
        {
            var sum = 0;
            foreach (var card in cards)
            {
                var colonIndex = card.IndexOf(':');
                var separatorIndex = card.IndexOf('|');
                var winners = card.Substring(colonIndex + 2, separatorIndex - (colonIndex + 3)).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var actual = card.Substring(separatorIndex + 1).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var points = 0;

                foreach (var number in actual)
                {
                    if (winners.Contains(number))
                    {
                        if (points == 0)
                        {
                            points++;
                        }
                        else
                        {
                            points *= 2;
                        }
                    }
                }
                sum += points;
            }
            return sum;
        }


        public override int SolvePart2(string[] cards)
        {
            var copiesWon = new Dictionary<int, int>();
            for (var i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                var firstSpaceIndex = card.IndexOf(' ');
                var colonIndex = card.IndexOf(':');
                var separatorIndex = card.IndexOf('|');
                var actualCardId = int.Parse(card.Substring(firstSpaceIndex + 1, colonIndex - firstSpaceIndex - 1));
                var winners = card.Substring(colonIndex + 2, separatorIndex - (colonIndex + 3)).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var actual = card.Substring(separatorIndex + 1).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                var winnersCount = 0;
                foreach (var number in actual)
                {
                    if (winners.Contains(number))
                    {
                        winnersCount++;
                    }
                }

                var copies = copiesWon.GetValueOrDefault(actualCardId, 0);
                copies++; //self ticket
                for (var j = 0; j < copies; j++)
                {
                    for (var k = 0; k < winnersCount; k++)
                    {
                        var copy = cards[k + i + 1];
                        firstSpaceIndex = copy.IndexOf(' ');
                        colonIndex = copy.IndexOf(':');
                        var cardId = int.Parse(copy.Substring(firstSpaceIndex + 1, colonIndex - firstSpaceIndex - 1));

                        if (copiesWon.ContainsKey(cardId))
                        {
                            copiesWon[cardId]++;
                        }
                        else
                        {
                            copiesWon[cardId] = 1;
                        }
                    }
                }
            }

            return cards.Length + copiesWon.Sum(c => c.Value);
        }
    }
}
