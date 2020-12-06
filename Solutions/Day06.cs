using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day06 : DayBase
    {
        protected override object SolvePart1()
        {
            var answers = InputComplete.Split("\r\n\r\n").Select(a => a.Replace("\r\n", string.Empty));
            return answers.Select(group => group.ToCharArray().Distinct().Count()).Sum();
        }

        protected override object SolvePart2()
        {
            var groups = InputComplete.Split("\r\n\r\n").Select(group => group.Split("\r\n").Select(group => group.ToCharArray()));

            var numberOfQuestionsEveryOneAnsweredYes = new List<int>();
            foreach (var group in groups)
            {
                IEnumerable<char> answeredYesByEveryone = null;
                foreach (var personAnswers in group)
                {
                    answeredYesByEveryone = answeredYesByEveryone == null ? personAnswers : personAnswers.Intersect(answeredYesByEveryone);
                }

                numberOfQuestionsEveryOneAnsweredYes.Add(answeredYesByEveryone.Count());
            }

            return numberOfQuestionsEveryOneAnsweredYes.Sum();
        }
    }
}
