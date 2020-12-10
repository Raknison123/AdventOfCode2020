using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day09 : DayBase
    {
        private readonly List<long> ciphers = new();

        public Day09()
        {
            this.ciphers = Input.Select(x => long.Parse(x)).ToList();
        }

        protected override object SolvePart1()
        {
            var preamble = 25;
            for (int i = preamble; i < this.ciphers.Count; i++)
            {
                var current = this.ciphers[i];
                var numbersToCompare = this.ciphers.Skip(i - preamble).Take(preamble).ToList();
                var sums = new List<long>();
                for (int j = 0; j < numbersToCompare.Count; j++)
                {
                    for (int k = j + 1; k < numbersToCompare.Count; k++)
                    {
                        sums.Add(current - numbersToCompare[j] - numbersToCompare[k]);
                    }
                }

                if (!sums.Any(x => x == 0))
                {
                    return current;
                }
            }

            return "No solution found!";
        }

        protected override object SolvePart2()
        {
            long invalidNumberToFind = (long)SolvePart1();
            for (int j = 0; j < this.ciphers.Count; j++)
            {
                long sum = this.ciphers[j];
                var numbers = new List<long>();
                numbers.Add(this.ciphers[j]);

                for (int k = j + 1; k < this.ciphers.Count; k++)
                {
                    numbers.Add(this.ciphers[k]);
                    sum += this.ciphers[k];

                    if (invalidNumberToFind == sum)
                    {
                        return numbers.Min() + numbers.Max();
                    }
                }
            }

            return "No solution found!";
        }
    }
}
