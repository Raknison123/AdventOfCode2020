using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day10 : DayBase
    {
        private readonly List<int> outputJoltage = new();

        public Day10()
        {
            this.outputJoltage = Input.Select(x => int.Parse(x)).ToList();
            this.outputJoltage.Add(0);
        }

        protected override object SolvePart1()
        {
            int countOf1JoltDiffs = 0;
            int countOf3JoltsDiffs = 0;
            var sortedJoltage = this.outputJoltage.OrderBy(x => x).ToList();
            sortedJoltage.Add(sortedJoltage.Max() + 3);

            for (int i = 1; i < sortedJoltage.Count; i++)
            {
                var diff = sortedJoltage[i] - sortedJoltage[i - 1];
                if (diff == 1)
                {
                    countOf1JoltDiffs++;
                }
                else if (diff == 3)
                {
                    countOf3JoltsDiffs++;
                }
            }

            return countOf1JoltDiffs * countOf3JoltsDiffs;
        }

        protected override object SolvePart2()
        {
            var sorted = this.outputJoltage.OrderBy(x => x).ToList();
            sorted.Add(sorted.Max() + 3);

            var combos = new Dictionary<long, long>();
            int counter = 1;
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                if (sorted[i + 1] - sorted[i] == 1)
                {
                    counter++;
                }
                else if (counter > 1)
                {
                    if (!combos.TryAdd(counter, 1))
                    {
                        combos[counter] += 1;
                    }

                    counter = 1;
                }
            }

            return combos.Aggregate((long)1, (accumulator, x) => accumulator *= (long)Math.Pow(GetCountOfCombinations(x.Key), x.Value));
        }


        private static int GetCountOfCombinations(long countOfOneJoltsDiffsInRow)
        {
            int result = 1;
            for (int i = 2; i <= countOfOneJoltsDiffsInRow; i++)
            {
                result += (i - 2);
            }

            return result;
        }
    }
}
