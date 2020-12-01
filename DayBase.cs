using System.IO;

namespace AdventOfCode2020
{
    public abstract class DayBase
    {
        protected string[] Input;

        public DayBase()
        {
            Input = File.ReadAllLines(@$"C:\temp\adventOfCode\2020\{this.GetType().Name}.txt");
        }

        public object Part1 => SolvePart1();
        public object Part2 => SolvePart2();

        protected abstract object SolvePart1();

        protected abstract object SolvePart2();
    }
}