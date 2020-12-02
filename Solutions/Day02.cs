using System;
using System.Linq;
using System.Text.RegularExpressions;

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
                string character = splittedPasswordLine[1].Substring(0, 1);
                string password = splittedPasswordLine[2];

                int numberOfOccurence = new Regex(character).Matches(password).Count;

                if (numberOfOccurence >= int.Parse(policy[0]) &&
                    numberOfOccurence <= int.Parse(policy[1]))
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
                string character = splittedPasswordLine[1].Substring(0, 1);
                string password = splittedPasswordLine[2];

                Regex checkCharPattern = new Regex($@"\G{character}");

                bool isOccurence1 = checkCharPattern.IsMatch(password, int.Parse(policy[0]) - 1);
                bool isOccurence2 = checkCharPattern.IsMatch(password, int.Parse(policy[1]) - 1);

                if (isOccurence1 ^ isOccurence2)
                {
                    isValidPassword++;
                }
            }

            return isValidPassword;
        }
    }
}
