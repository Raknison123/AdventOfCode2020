using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day12 : DayBase
    {
        private List<(string direction, int value)> actions;

        public Day12()
        {
            this.actions = Input.Select(a => (a.Substring(0, 1), int.Parse(a.Substring(1, a.Length - 1)))).ToList();
        }

        protected override object SolvePart1()
        {
            var ship = new ShipPart1();
            foreach (var (direction, value) in this.actions)
            {
                ship.Move(direction, value);
            }

            return ship.X + ship.Y;
        }

        protected override object SolvePart2()
        {
            var ship = new ShipPart2();
            foreach (var (direction, value) in this.actions)
            {
                ship.Move(direction, value);
            }

            return ship.X + ship.Y;
        }
    }

    class ShipPart1
    {
        private int facingDirection = 90;

        private Dictionary<int, (int multiX, int multiY)> multiplicators = new()
        {
            { 0, (0, -1) },
            { 90, (1, 0) },
            { 180, (0, 1) },
            { 270, (-1, 0) },
        };

        public int X { get; set; }
        public int Y { get; set; }

        public void Move(string direction, int value)
        {
            switch (direction)
            {
                case "N":
                    Y -= value;
                    break;
                case "S":
                    Y += value;
                    break;
                case "E":
                    X += value;
                    break;
                case "W":
                    X -= value;
                    break;
                case "L":
                    facingDirection = (facingDirection + (360 - value)) % 360;
                    break;
                case "R":
                    facingDirection = (facingDirection + value) % 360;
                    break;
                case "F":
                    var (multiX, multiY) = multiplicators[facingDirection];
                    X += multiX * value;
                    Y += multiY * value;
                    break;
            }
        }
    }

    class ShipPart2
    {
        private (int x, int y) wayPoint = (10, -1);

        private Dictionary<int, (int multiX, int multiY)> multiplicators = new()
        {
            { 0, (0, -1) },
            { 90, (1, 0) },
            { 180, (0, 1) },
            { 270, (-1, 0) },
        };

        public int X { get; set; }
        public int Y { get; set; }

        public void Move(string direction, int value)
        {
            switch (direction)
            {
                case "N":
                    wayPoint.y -= value;
                    break;
                case "S":
                    wayPoint.y += value;
                    break;
                case "E":
                    wayPoint.x += value;
                    break;
                case "W":
                    wayPoint.x -= value;
                    break;
                case "L":
                case "R":
                    RotateWayPoint(direction, value);
                    break;
                case "F":
                    X += wayPoint.x * value;
                    Y += wayPoint.y * value;
                    break;
            }
        }

        private void RotateWayPoint(string direction, int value)
        {
            int angle = direction == "L" ? (360 - value) % 360 : value % 360;
            var diffX = wayPoint.x;
            var diffY = wayPoint.y;

            switch (angle)
            {
                case 90:
                    wayPoint.x = -diffY;
                    wayPoint.y = diffX;
                    break;
                case 180:
                    wayPoint.x = -diffX;
                    wayPoint.y = -diffY;
                    break;
                case 270:
                    wayPoint.x = diffY;
                    wayPoint.y = -diffX;
                    break;
            }
        }
    }
}
