using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day15 : DayBase
    {
        protected override object SolvePart1()
        {
            return GetNumber(2020);
        }

        protected override object SolvePart2()
        {
            return GetNumber(30000000);
        }

        private int GetNumber(int rounds)
        {
            List<int> inputs = this.InputComplete.Split(",").Select(x => int.Parse(x)).ToList();
            Dictionary<int, (int? previous, int last)> results = new();
            int add = 0;
            for (int i = 1; i <= rounds; i++)
            {
                if (inputs.Count >= i)
                {
                    add = inputs[i - 1];
                    results.TryAdd(add, (null, i));
                    continue;
                }

                if (results.TryGetValue(add, out (int? previous, int last) stored) && stored.previous.HasValue)
                {
                    add = stored.last - stored.previous.Value;
                    if (!results.TryAdd(add, new(null, i)))
                    {
                        results[add] = (results[add].last, i);
                    }
                }
                else
                {
                    add = 0;
                    if (!results.TryAdd(add, (null, i)))
                    {
                        results[add] = (results[add].last, i);
                    }
                }
            }

            return add;
        }
    }
}
