using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day08 : DayBase
    {
        private readonly List<(string operation, int value)> instructions = new();

        public Day08()
        {
            this.instructions = Input.Select(x => (operation: x.Split(' ')[0], value: int.Parse(x.Split(' ')[1]))).ToList();
        }

        protected override object SolvePart1()
        {
            return RunProgram(this.instructions);
        }

        protected override object SolvePart2()
        {
            for (int i = 0; i < this.instructions.Count; i++)
            {
                var modifiedInstructions = new List<(string operation, int value)>(this.instructions);
                modifiedInstructions[i] = modifiedInstructions[i].operation == "nop" ? ("jmp", modifiedInstructions[i].value) : modifiedInstructions[i];
                modifiedInstructions[i] = modifiedInstructions[i].operation == "jmp" ? ("nop", modifiedInstructions[i].value) : modifiedInstructions[i];

                var (isNormalTerminate, accumulator) = RunProgram(modifiedInstructions);
                if (isNormalTerminate) return accumulator;
            }

            return "No solution found!";
        }

        private static (bool isNormalTerminate, int accumulator) RunProgram(List<(string operation, int value)> instructions)
        {
            var visitedOperations = new HashSet<int>();
            int index = 0;
            int accumulator = 0;

            while (true)
            {
                if (instructions.Count <= index)
                {
                    return (isNormalTerminate: true, accumulator);
                }

                var (operation, value) = instructions[index];
                if (!visitedOperations.Contains(index))
                {
                    visitedOperations.Add(index);
                }
                else
                {
                    return (isNormalTerminate: false, accumulator);
                }

                switch (operation)
                {
                    case "acc":
                        accumulator += value;
                        index++;
                        break;

                    case "nop":
                        index++;
                        break;

                    case "jmp":
                        index += value;
                        break;
                }
            }
        }
    }
}
