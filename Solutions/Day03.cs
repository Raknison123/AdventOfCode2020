using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day03 : DayBase
    {
        private readonly Dictionary<(int x, int y), FieldType> field;
        private readonly int fieldWidth;
        private readonly int fieldHeight;

        public Day03()
        {
            this.field = GetField();
            this.fieldWidth = this.field.Max(f => f.Key.x) + 1;
            this.fieldHeight = this.field.Max(f => f.Key.y) + 1;
        }

        protected override object SolvePart1()
        {
            return TraverseMap(3, 1);
        }

        protected override object SolvePart2()
        {
            {
                return TraverseMap(1, 1) *
                       TraverseMap(3, 1) *
                       TraverseMap(5, 1) *
                       TraverseMap(7, 1) *
                       TraverseMap(1, 2);
            }
        }

        private long TraverseMap(int xOffset, int yOffset)
        {
            (int x, int y) tobogganPosition = (0, 0);
            long numberOfEncounteredTrees = 0;
            do
            {
                tobogganPosition.x += xOffset;
                tobogganPosition.y += yOffset;

                if (this.field.TryGetValue((tobogganPosition.x % fieldWidth, tobogganPosition.y), out FieldType currentField))
                {
                    if (currentField == FieldType.Tree)
                    {
                        numberOfEncounteredTrees++;
                    }
                }

            } while (tobogganPosition.y < this.fieldHeight);

            return numberOfEncounteredTrees;
        }

        private Dictionary<(int x, int y), FieldType> GetField()
        {
            var field = new Dictionary<(int x, int y), FieldType>();
            for (int y = 0; y < Input.Length; y++)
            {
                var row = Input[y].ToCharArray();

                for (int x = 0; x < row.Length; x++)
                {
                    field.Add((x, y), row[x] == '#' ? FieldType.Tree : FieldType.Open);
                }
            }

            return field;
        }
    }
    enum FieldType
    {
        Open,
        Tree,
    }
}
