using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day06 : DayBase
    {
        protected override object SolvePart1()
        {
            var groups = InputComplete.Split("\r\n\r\n").ToList();
            return groups.Select(group => group.Replace("\r\n", "").Distinct().Count()).Sum();
        }

        protected override object SolvePart2()
        {
            var groups = InputComplete.Split("\r\n\r\n").Select(group => group.Split("\r\n"));

            var numberOfQuestionsEveryOneAnsweredYes = new List<int>();
            foreach (var group in groups)
            {
                IEnumerable<char> answeredYesByEveryone = null;
                foreach (string personAnswers in group)
                {
                    answeredYesByEveryone = answeredYesByEveryone == null ? personAnswers.ToCharArray() : personAnswers.Intersect(answeredYesByEveryone);
                }

                numberOfQuestionsEveryOneAnsweredYes.Add(answeredYesByEveryone.Count());
            }

            return numberOfQuestionsEveryOneAnsweredYes.Sum();
        }
    }
}
