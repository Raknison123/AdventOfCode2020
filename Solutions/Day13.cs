using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day13 : DayBase
    {
        private List<string> busIds;
        private int timeOfDeparture;

        public Day13()
        {
            timeOfDeparture = int.Parse(this.Input[0]);
            this.busIds = Input[1].Split(',').ToList();
        }

        protected override object SolvePart1()
        {
            var minutesToWait = new Dictionary<int, int>();
            foreach (var id in this.busIds.Where(id => id != "x").Select(id => int.Parse(id)))
            {
                var divided = (decimal)timeOfDeparture / id;
                var waitMin = (int)Math.Round((1 - (divided - (int)divided)) * id, 0);
                minutesToWait.Add(id, waitMin);
            }

            var result = minutesToWait.OrderBy(x => x.Value).First();
            return result.Key * result.Value;
        }

        protected override object SolvePart2()
        {
            int offSet = 1;
            long searchResult = 1;
            long increment = 1;
            long firstId = long.Parse(this.busIds.First());
            foreach (var id in this.busIds.Skip(1))
            {
                if (id != "x")
                {
                    long currentId = long.Parse(id);
                    long i = searchResult;

                    while (true)
                    {
                        if ((i + offSet) % currentId == 0 && (i % firstId) == 0) break;
                        i += increment;
                    }
                    searchResult = i;
                    increment = GetLeastCommonMultiple(increment == 1 ? firstId : increment, currentId);
                }
                offSet++;
            }

            return searchResult;
        }

        public long GetLeastCommonMultiple(long number1, long number2)
        {
            return (number1 * number2) / GetGreatestCommonDivisor(number1, number2);
        }
        public long GetGreatestCommonDivisor(long number1, long number2)
        {
            while (number1 % number2 != 0)
            {
                long temp = number1 % number2;
                number1 = number2;
                number2 = temp;
            }

            long ggt = number2;
            return ggt;
        }
    }
}
