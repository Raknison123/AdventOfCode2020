using System;

namespace AdventOfCode2020.Solutions
{
    public class Day01 : DayBase
    {
        protected override object SolvePart1()
        {
            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = i; j < Input.Length; j++)
                {
                    int value1 = Convert.ToInt32(Input[i]);
                    int value2 = Convert.ToInt32(Input[j]);

                    if (value1 + value2 == 2020)
                    {
                        return value1 * value2;
                    }
                }
            }

            return 0;
        }

        protected override object SolvePart2()
        {
            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = i; j < Input.Length; j++)
                {
                    for (int k = j; k < Input.Length; k++)
                    {
                        int value1 = Convert.ToInt32(Input[i]);
                        int value2 = Convert.ToInt32(Input[j]);
                        int value3 = Convert.ToInt32(Input[k]);

                        if (value1 + value2 + value3 == 2020)
                        {
                            return value1 * value2 * value3;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
