using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day11 : DayBase
    {
        private Dictionary<(int x, int y), State> planeSeats;

        private List<(int offsetX, int offSetY)> checkSeatDirections = new()
        {
            (0, -1),
            (0, 1),
            (1, 0),
            (1, -1),
            (1, 1),
            (-1, 0),
            (-1, -1),
            (-1, 1),
        };

        private int fieldHeight;
        private int fieldWidth;


        protected override object SolvePart1()
        {
            InitializeSeats();
            var countOfCurrentOccupiedSeats = 0;
            int countOfNewOccupiedSeats = 0;
            int countOfIterations = 0;

            do
            {
                countOfIterations++;
                countOfCurrentOccupiedSeats = this.planeSeats.Count(seat => seat.Value == State.Occupied);
                this.planeSeats = NextSeatingRound(fieldWidth, fieldHeight, 4);
                countOfNewOccupiedSeats = this.planeSeats.Count(seat => seat.Value == State.Occupied);
            }
            while (countOfCurrentOccupiedSeats != countOfNewOccupiedSeats);

            return countOfCurrentOccupiedSeats;
        }

        protected override object SolvePart2()
        {
            InitializeSeats();
            var countOfCurrentOccupiedSeats = 0;
            int countOfNewOccupiedSeats = 0;
            int countOfIterations = 0;
            int fieldHeight = this.planeSeats.Max(plane => plane.Key.y);
            int fieldWidth = this.planeSeats.Max(plane => plane.Key.x);
            do
            {
                countOfIterations++;
                countOfCurrentOccupiedSeats = this.planeSeats.Count(seat => seat.Value == State.Occupied);
                this.planeSeats = NextSeatingRound(fieldWidth, fieldHeight, 5, true);
                countOfNewOccupiedSeats = this.planeSeats.Count(seat => seat.Value == State.Occupied);
            }
            while (countOfCurrentOccupiedSeats != countOfNewOccupiedSeats);

            return countOfCurrentOccupiedSeats;
        }

        private Dictionary<(int x, int y), State> NextSeatingRound(int fieldWidth, int fieldHeight, int seatOccupiedThreshold, bool isPartB = false)
        {
            Dictionary<(int x, int y), State> newPlaneSeats = new();

            for (int y = 0; y <= fieldHeight; y++)
            {
                for (int x = 0; x <= fieldWidth; x++)
                {
                    State currentSeat = this.planeSeats.GetValueOrDefault((x, y));

                    Dictionary<(int x, int y), State> relevantSeats = isPartB ? GetVisibleSeats(x, y) : GetAdjacentSeats(x, y);
                    if (currentSeat == State.Empty && !relevantSeats.Any(s => s.Value == State.Occupied))
                    {
                        newPlaneSeats[(x, y)] = State.Occupied;
                    }
                    else if (currentSeat == State.Occupied && relevantSeats.Count(s => s.Value == State.Occupied) >= seatOccupiedThreshold)
                    {
                        newPlaneSeats[(x, y)] = State.Empty;
                    }
                    else
                    {
                        newPlaneSeats[(x, y)] = this.planeSeats.GetValueOrDefault((x, y));
                    }
                }
            }

            return newPlaneSeats;
        }

        private Dictionary<(int x, int y), State> GetVisibleSeats(int x, int y)
        {
            Dictionary<(int x, int y), State> relevantSeats = new();

            foreach (var (offsetX, offSetY) in checkSeatDirections)
            {
                var newX = x + offsetX;
                var newY = y + offSetY;
                while (newX <= this.fieldWidth && newX >= 0 && newY <= this.fieldHeight && newY >= 0)
                {
                    var seatToCheck = this.planeSeats.GetValueOrDefault((newX, newY));
                    if (seatToCheck == State.Occupied)
                    {
                        relevantSeats.Add((newX, newY), State.Occupied);
                        break;
                    }
                    else if (seatToCheck == State.Empty)
                    {
                        break;
                    }

                    newX += offsetX;
                    newY += offSetY;
                }
            }
            return relevantSeats;
        }

        private Dictionary<(int x, int y), State> GetAdjacentSeats(int x, int y)
        {
            Dictionary<(int x, int y), State> relevantSeats = new();
            foreach (var (offsetX, offSetY) in checkSeatDirections)
            {
                var newX = x + offsetX;
                var newY = y + offSetY;
                relevantSeats.Add((newX, newY), this.planeSeats.GetValueOrDefault((newX, newY)));
            }

            return relevantSeats;
        }

        enum State
        {
            Floor,
            Empty,
            Occupied,
        }

        public void InitializeSeats()
        {
            this.planeSeats = new Dictionary<(int x, int y), State>();
            for (int y = 0; y < Input.Length; y++)
            {
                var row = Input[y].ToCharArray();
                for (int x = 0; x < row.Length; x++)
                {
                    planeSeats.Add((x, y), row[x] == 'L' ? State.Empty : State.Floor);
                }
            }

            this.fieldWidth = this.planeSeats.Max(plane => plane.Key.x);
            this.fieldHeight = this.planeSeats.Max(plane => plane.Key.y);
        }
    }
}
