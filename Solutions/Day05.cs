using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day05 : DayBase
    {
        protected override object SolvePart1()
        {
            List<int> seatIds = CalculateSeatIds();
            return seatIds.Max();
        }

        protected override object SolvePart2()
        {
            List<int> seatIds = CalculateSeatIds();
            var ourSeatId = seatIds.Aggregate((ourSeat, nextSeat) => nextSeat - 1 == ourSeat ? ++ourSeat : ourSeat) + 1;
            return ourSeatId;
        }

        private List<int> CalculateSeatIds()
        {
            return Input.Select(line => CalculateSeat(line)).OrderBy(x => x).ToList();
        }

        private int CalculateSeat(string line)
        {
            int minRow = 0;
            int maxRow = 127;

            int minSeat = 0;
            int maxSeat = 7;

            foreach (var character in line.ToCharArray())
            {
                switch (character)
                {
                    case 'F':
                        maxRow -= ((maxRow - minRow) / 2) + 1;
                        break;
                    case 'B':
                        minRow += ((maxRow - minRow) / 2) + 1;
                        break;
                    case 'R':
                        minSeat += ((maxSeat - minSeat) / 2) + 1;
                        break;
                    case 'L':
                        maxSeat -= ((maxSeat - minSeat) / 2) + 1;
                        break;
                }
            }

            return minRow * 8 + minSeat;
        }
    }
}
