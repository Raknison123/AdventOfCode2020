using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    public class Day14 : DayBase
    {
        protected override object SolvePart1()
        {
            char[] mask = Array.Empty<char>();
            Dictionary<int, long> memory = new Dictionary<int, long>();

            foreach (var row in this.Input)
            {
                var splitted = row.Split("=");
                if (splitted[0].StartsWith("mask"))
                {
                    mask = row.Split("=")[1].Trim().ToCharArray();
                }
                else
                {
                    int saveAddress = int.Parse(splitted[0].Substring(splitted[0].IndexOf('[') + 1, splitted[0].IndexOf(']') - splitted[0].IndexOf('[') - 1));
                    var value = Convert.ToString(int.Parse(splitted[1]), 2).PadLeft(36, '0').ToCharArray();

                    foreach (var i in Enumerable.Range(0, mask.Length))
                    {
                        value[i] = mask[i] != 'X' ? mask[i] : value[i];
                    }

                    long valueToSave = Convert.ToInt64(string.Join("", value), 2);
                    if (!memory.TryAdd(saveAddress, valueToSave))
                    {
                        memory[saveAddress] = valueToSave;
                    }
                }
            }

            return memory.Sum(x => x.Value);
        }

        protected override object SolvePart2()
        {
            char[] mask = Array.Empty<char>();
            Dictionary<long, long> memory = new Dictionary<long, long>();
            foreach (var row in this.Input)
            {
                var splitted = row.Split("=");
                if (splitted[0].StartsWith("mask"))
                {
                    mask = row.Split("=")[1].Trim().ToCharArray();
                }
                else
                {
                    int address = int.Parse(splitted[0].Substring(splitted[0].IndexOf('[') + 1, splitted[0].IndexOf(']') - splitted[0].IndexOf('[') - 1));
                    var addressBits = Convert.ToString(address, 2).PadLeft(36, '0').ToCharArray();

                    foreach (var i in Enumerable.Range(0, mask.Length))
                    {
                        addressBits[i] = mask[i] == 'X' || mask[i] == '1' ? mask[i] : addressBits[i];
                    }

                    var addresses = new List<List<char>>();
                    GetSaveAddresses(addressBits.ToList(), addresses);

                    foreach (var addr in addresses)
                    {
                        long addre = Convert.ToInt64(string.Join("", addr), 2);
                        if (!memory.TryAdd(addre, long.Parse(splitted[1])))
                        {
                            memory[addre] = long.Parse(splitted[1]);
                        }
                    }
                }
            }

            return memory.Sum(x => x.Value);
        }

        private void GetSaveAddresses(List<char> address, List<List<char>> addresses)
        {
            for (int i = 0; i < address.Count; i++)
            {
                if (address[i] == 'X')
                {
                    var copy1 = new List<char>(address);
                    var copy2 = new List<char>(address);
                    copy1[i] = '1';
                    copy2[i] = '0';
                    GetSaveAddresses(copy1, addresses);
                    GetSaveAddresses(copy2, addresses);
                    return;
                }
            }

            addresses.Add(address);
        }
    }
}
