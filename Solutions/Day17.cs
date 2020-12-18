using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day17 : DayBase
    {
        private Dictionary<(int x, int y, int z), Status> dimensions3 = new();
        private Dictionary<(int x, int y, int z, int w), Status> dimensions4 = new();

        public Day17()
        {
            for (int y = 0; y < this.Input.Length; y++)
            {
                int x = 0;
                foreach (var cube in this.Input[y].ToCharArray())
                {
                    dimensions3.Add((x, y, 0), cube == '#' ? Status.Active : Status.Inactive);
                    dimensions4.Add((x, y, 0, 0), cube == '#' ? Status.Active : Status.Inactive);
                    x++;
                }
            }
        }

        protected override object SolvePart1()
        {
            int numberOfCycles = 6;
            for (int i = 0; i < numberOfCycles; i++)
            {
                Dictionary<(int x, int y, int z), Status> newDimensions = new();

                for (int x = this.dimensions3.Keys.Min(k => k.x) - 1; x <= this.dimensions3.Keys.Max(k => k.x) + 1; x++)
                {
                    for (int y = this.dimensions3.Keys.Min(k => k.y) - 1; y <= this.dimensions3.Keys.Max(k => k.y) + 1; y++)
                    {
                        for (int z = this.dimensions3.Keys.Min(k => k.z) - 1; z <= this.dimensions3.Keys.Max(k => k.z) + 1; z++)
                        {
                            this.dimensions3.TryGetValue((x, y, z), out Status cubeValue);
                            int countActiveNeighbours = 0;

                            foreach (var neighbourCoord in GetNeighbourCoordinates3D((x, y, z)))
                            {
                                countActiveNeighbours += this.dimensions3.TryGetValue(neighbourCoord, out Status neighbour1) && neighbour1 == Status.Active ? 1 : 0;
                            }

                            if (cubeValue == Status.Active)
                            {
                                newDimensions.Add((x, y, z), countActiveNeighbours == 2 || countActiveNeighbours == 3 ? Status.Active : Status.Inactive);
                            }
                            else
                            {
                                if (countActiveNeighbours == 3)
                                {
                                    newDimensions.Add((x, y, z), Status.Active);
                                }
                            }
                        }
                    }
                }

                this.dimensions3 = newDimensions;
            }

            return this.dimensions3.Count(cube => cube.Value == Status.Active);
        }

        protected override object SolvePart2()
        {
            int numberOfCycles = 6;
            for (int i = 0; i < numberOfCycles; i++)
            {
                Dictionary<(int x, int y, int z, int w), Status> newDimensions = new();

                // Yes it is reeeeeally ugly!!
                for (int x = this.dimensions4.Keys.Min(k => k.x) - 1; x <= this.dimensions4.Keys.Max(k => k.x) + 1; x++)
                {
                    for (int y = this.dimensions4.Keys.Min(k => k.y) - 1; y <= this.dimensions4.Keys.Max(k => k.y) + 1; y++)
                    {
                        for (int z = this.dimensions4.Keys.Min(k => k.z) - 1; z <= this.dimensions4.Keys.Max(k => k.z) + 1; z++)
                        {
                            for (int w = this.dimensions4.Keys.Min(k => k.w) - 1; w <= this.dimensions4.Keys.Max(k => k.w) + 1; w++)
                            {
                                this.dimensions4.TryGetValue((x, y, z, w), out Status cubeValue);
                                int countActiveNeighbours = 0;

                                foreach (var neighbourCoord in GetNeighbourCoordinates4D((x, y, z, w)))
                                {
                                    countActiveNeighbours += this.dimensions4.TryGetValue(neighbourCoord, out Status neighbour1) && neighbour1 == Status.Active ? 1 : 0;
                                }

                                if (cubeValue == Status.Active)
                                {
                                    newDimensions.Add((x, y, z, w), countActiveNeighbours == 2 || countActiveNeighbours == 3 ? Status.Active : Status.Inactive);
                                }
                                else
                                {
                                    if (countActiveNeighbours == 3)
                                    {
                                        newDimensions.Add((x, y, z, w), Status.Active);
                                    }
                                }
                            }
                        }
                    }
                }

                this.dimensions4 = newDimensions;
            }

            return this.dimensions4.Count(cube => cube.Value == Status.Active);
        }

        private List<(int x, int y, int z)> GetNeighbourCoordinates3D((int x, int y, int z) key)
        {
            var coords = new List<(int x, int y, int z)>();

            // Yes it is reeeeeally ugly!!
            for (int xOffSet = -1; xOffSet <= 1; xOffSet++)
            {
                for (int yOffSet = -1; yOffSet <= 1; yOffSet++)
                {
                    for (int zOffSet = -1; zOffSet <= 1; zOffSet++)
                    {
                        if (zOffSet == 0 && yOffSet == 0 && xOffSet == 0) continue;
                        coords.Add((key.x + xOffSet, key.y + yOffSet, key.z + zOffSet));
                    }
                }
            }

            return coords;
        }

        private List<(int x, int y, int z, int w)> GetNeighbourCoordinates4D((int x, int y, int z, int w) key)
        {
            var coords = new List<(int x, int y, int z, int w)>();

            // Yes it is reeeeeally ugly!!
            for (int xOffSet = -1; xOffSet <= 1; xOffSet++)
            {
                for (int yOffSet = -1; yOffSet <= 1; yOffSet++)
                {
                    for (int zOffSet = -1; zOffSet <= 1; zOffSet++)
                    {
                        for (int wOffSet = -1; wOffSet <= 1; wOffSet++)
                        {
                            if (zOffSet == 0 && yOffSet == 0 && xOffSet == 0 && wOffSet == 0) continue;
                            coords.Add((key.x + xOffSet, key.y + yOffSet, key.z + zOffSet, key.w + wOffSet));
                        }
                    }
                }
            }

            return coords;
        }

        enum Status
        {
            Inactive,
            Active,
        }
    }
}