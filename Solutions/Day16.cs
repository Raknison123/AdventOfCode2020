using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day16 : DayBase
    {
        private Dictionary<string, List<(int from, int to)>> rules = new();
        private List<int> myTicket = new();
        private List<List<int>> nearbyTickets = new();
        private List<List<int>> validTickets = new();

        public Day16()
        {
            var sections = this.InputComplete.Split("\r\n\r\n");

            this.rules = sections[0].Split("\r\n")
                               .Select(row => row.Split(": "))
                               .ToDictionary(kvp => kvp[0], kvp => kvp[1].Split(" or ")
                                    .Select(r => r.Split("-"))
                                    .Select(num => (int.Parse(num[0]), int.Parse(num[1]))).ToList());

            this.myTicket = sections[1].Split("\r\n")[1].Split(",").Select(x => int.Parse(x)).ToList();

            this.nearbyTickets = sections[2].Split("\r\n").Skip(1).ToList()
                                            .Select(x => x.Split(",").Select(x => int.Parse(x)).ToList()).ToList();
        }

        protected override object SolvePart1()
        {
            var invalidTicketRules = new List<int>();
            foreach (var ticket in this.nearbyTickets)
            {
                bool isValidTicket = true;
                foreach (var field in ticket)
                {
                    if (!this.rules.Values.Any(x => x.Any(y => y.from <= field && y.to >= field)))
                    {
                        invalidTicketRules.Add(field);
                        isValidTicket = false;
                    }
                }
                if (isValidTicket)
                {
                    this.validTickets.Add(ticket);
                }
            }

            return invalidTicketRules.Sum();
        }

        protected override object SolvePart2()
        {
            Dictionary<string, List<long>> matchingRulesForColumns = new();
            for (int i = 0; i < this.validTickets[0].Count; i++)
            {
                // Determine the field for a certain column
                foreach (var rule in this.rules)
                {
                    var columnValues = this.validTickets.Select(vt => vt[i]);
                    if (columnValues.All(val => rule.Value.Any(rule => rule.from <= val && rule.to >= val)))
                    {
                        if (!matchingRulesForColumns.TryAdd(rule.Key, new List<long> { this.myTicket[i] }))
                        {
                            matchingRulesForColumns[rule.Key].Add(this.myTicket[i]);
                        }
                    }
                }
            }

            var result = new Dictionary<string, long>();
            foreach (var rule in matchingRulesForColumns.OrderBy(x => x.Value.Count))
            {
                var firstValue = rule.Value.First();
                result.Add(rule.Key, firstValue);
                matchingRulesForColumns.Values.Select(x => x.Remove(firstValue)).ToList();
            }

            return result.Where(r => r.Key.StartsWith("departure")).Aggregate((long)1, (x, y) => x * y.Value);
        }
    }
}

