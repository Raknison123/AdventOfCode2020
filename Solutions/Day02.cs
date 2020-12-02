using System;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day02 : DayBase
    {
        protected override object SolvePart1()
        {
            int isValidPassword = 0;

            foreach (string passwordLine in this.Input)
            {
                string[] splittedPasswordLine = passwordLine.Split(' ');
                string[] policy = splittedPasswordLine[0].Split('-');
                int position1Policy = int.Parse(policy[0]);
                int position2Policy = int.Parse(policy[1]);
                char character = splittedPasswordLine[1].Substring(0, 1).ToCharArray()[0];
                string password = splittedPasswordLine[2];

                int numberOfOccurence = password.Count(p => p == character);
                if (numberOfOccurence >= position1Policy &&
                    numberOfOccurence <= position2Policy)
                {
                    isValidPassword++;
                }
            }

            return isValidPassword;
        }

        protected override object SolvePart2()
        {
            int isValidPassword = 0;

            foreach (string passwordLine in this.Input)
            {
                string[] splittedPasswordLine = passwordLine.Split(' ');
                string[] policy = splittedPasswordLine[0].Split('-');
                int position1Policy = int.Parse(policy[0]);
                int position2Policy = int.Parse(policy[1]);
                string character = splittedPasswordLine[1].Substring(0, 1);
                string password = splittedPasswordLine[2];

                bool isOccurence1 = password.Substring(position1Policy - 1, 1) == character;
                bool isOccurence2 = password.Substring(position2Policy - 1, 1) == character;

                if (isOccurence1 ^ isOccurence2)
                {
                    isValidPassword++;
                }
            }

            return isValidPassword;
        }
    }
}
