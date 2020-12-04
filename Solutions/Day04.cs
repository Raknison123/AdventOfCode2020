using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Solutions
{
    public class Day04 : DayBase
    {
        protected override object SolvePart1()
        {
            var passports = GetPassports();
            int numberOfValidPassports = 0;
            foreach (var passport in passports)
            {
                if (HasPassportAllNecessaryInfos(passport)) numberOfValidPassports++;
            }

            return numberOfValidPassports;
        }

        protected override object SolvePart2()
        {
            var passports = GetPassports();
            int numberOfValidPassports = 0;
            foreach (var passport in passports)
            {
                if (!HasPassportAllNecessaryInfos(passport)) continue;

                var birthYear = int.Parse(passport["byr"]);
                if (!(birthYear >= 1920 && birthYear <= 2002)) continue;

                var issueYear = int.Parse(passport["iyr"]);
                if (!(issueYear >= 2010 && issueYear <= 2020)) continue;

                var expYear = int.Parse(passport["eyr"]);
                if (!(expYear >= 2020 && expYear <= 2030)) continue;

                var heightComplete = passport["hgt"];
                int height = 0;
                if (heightComplete.EndsWith("cm"))
                {
                    height = int.Parse(heightComplete[0..^2]);
                    if (!(height >= 150 && height <= 193)) continue;
                }
                else if (heightComplete.EndsWith("in"))
                {
                    height = int.Parse(heightComplete[0..^2]);
                    if (!(height >= 59 && height <= 76)) continue;
                }
                else continue;

                var hcl = passport["hcl"];
                var regex = new Regex("^[#][a-f0-9#]{6,6}$");
                if (!regex.IsMatch(hcl)) continue;

                var ecl = passport["ecl"];
                switch (ecl)
                {
                    case "amb":
                    case "blu":
                    case "brn":
                    case "gry":
                    case "grn":
                    case "hzl":
                    case "oth":
                        break;
                    default:
                        continue;
                }

                var pid = passport["pid"];
                if (!(pid.Length == 9 && int.TryParse(pid, out int pidNumber))) continue;

                numberOfValidPassports++;
            }

            return numberOfValidPassports;
        }

        private static bool HasPassportAllNecessaryInfos(Dictionary<string, string> passport)
        {
            return passport.ContainsKey("byr") &&
                                passport.ContainsKey("iyr") &&
                                passport.ContainsKey("eyr") &&
                                passport.ContainsKey("hgt") &&
                                passport.ContainsKey("hcl") &&
                                passport.ContainsKey("ecl") &&
                                passport.ContainsKey("pid");
        }

        private List<Dictionary<string, string>> GetPassports()
        {
            var passports = new List<Dictionary<string, string>>();
            foreach (var passport in InputComplete.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                passports.Add(passport.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries)
                                           .ToDictionary(x => x.Split(":")[0], x => x.Split(":")[1]));
            }

            return passports;
        }
    }
}
