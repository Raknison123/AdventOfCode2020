using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day07 : DayBase
    {
        private readonly Dictionary<string, List<(int amount, string bagType)>> bagPolicies = new Dictionary<string, List<(int amount, string bagType)>>();
        private readonly HashSet<string> bagColorsContainShinyGold = new HashSet<string>();
        private int numOfRequiredBagsInside = 0;

        public Day07()
        {
            foreach (var policyLine in Input)
            {
                var cleanedPolicyLine = policyLine.Replace(" bags", "").Replace(" bag", "").Replace(".", "");
                var policy = cleanedPolicyLine.Split(" contain ");
                var contains = policy[1].Split(", ").Where(x => x != "no other")
                                        .Select(x => (amount: int.Parse(x.Substring(0, 1)), bagType: x.Substring(1).Trim()))
                                        .ToList();

                this.bagPolicies.Add(policy[0], contains);
            }
        }

        protected override object SolvePart1()
        {
            foreach (var bagPolicyPair in this.bagPolicies)
            {
                foreach (var bagPolicy in bagPolicyPair.Value)
                {
                    FindContainedBagColors(
                        bagPolicy,
                        new HashSet<string>
                        {
                            bagPolicyPair.Key,
                        });
                }
            }

            return this.bagColorsContainShinyGold.Count;
        }

        protected override object SolvePart2()
        {
            this.numOfRequiredBagsInside = 0;
            var bagPolicyPair = this.bagPolicies.Single(p => p.Key == "shiny gold");
            {
                foreach (var bagPolicy in bagPolicyPair.Value)
                {
                    HashSet<string> visitedBagTypes = new HashSet<string>
                    {
                        bagPolicyPair.Key
                    };

                    FindContainedBagColors(bagPolicy, visitedBagTypes);
                }
            }

            return this.numOfRequiredBagsInside;
        }

        private void FindContainedBagColors((int amount, string bagType) bagPolicy, HashSet<string> visitedBagTypes)
        {
            this.numOfRequiredBagsInside += bagPolicy.amount;
            if (bagPolicy.bagType == "shiny gold")
            {
                foreach (var bagType in visitedBagTypes)
                {
                    this.bagColorsContainShinyGold.Add(bagType);
                }
            }

            visitedBagTypes.Add(bagPolicy.bagType);
            if (bagPolicies.TryGetValue(bagPolicy.bagType, out List<(int amount, string bagType)> containsBags))
            {
                foreach (var (amount, bagType) in containsBags)
                {
                    FindContainedBagColors((amount: amount * bagPolicy.amount, bagType), new HashSet<string>(visitedBagTypes));
                }
            }
        }
    }
}
