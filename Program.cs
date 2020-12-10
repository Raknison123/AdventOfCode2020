using AdventOfCode2020.Solutions;
using System;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            //var puzzle = new Day01();
            var puzzle = new Day10();

            Console.WriteLine($"{puzzle.GetType().Name} - Part1:{puzzle.Part1}, Part2:{puzzle.Part2}");
            Console.ReadKey();
        }
    }
}
