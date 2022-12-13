using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day11
{
    public class Day11Solutions
    {
        public static void Part1()
        {
            ulong i, rounds = 20;
            Monkey.WorrySuppressor = 3;
            Dictionary<string, Monkey> monkeys = LoadMonkeys("input.txt");
            Console.WriteLine();
            for (i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys.Values)
                {
                    monkey.ThrowAllItems(monkeys);
                }
            }
            List<ulong> totalNumberOfInspections = monkeys.Values.Select(monkey => monkey.TotalNumberOfInspections).ToList();
            totalNumberOfInspections.Sort();
            totalNumberOfInspections.Reverse();
            ulong monkeyBusiness = totalNumberOfInspections[0] * totalNumberOfInspections[1];
            Console.WriteLine($"Day 11, Part 1 Solution: {monkeyBusiness}");
        }
        public static void Part2()
        {
            ulong i, rounds = 10000;
            Monkey.WorrySuppressor = 1;
            Dictionary<string, Monkey> monkeys = LoadMonkeys("input.txt");
            for (i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys.Values)
                {
                    monkey.ThrowAllItems(monkeys);
                }
            }
            List<ulong> totalNumberOfInspections = monkeys.Values.Select(monkey => monkey.TotalNumberOfInspections).ToList();
            totalNumberOfInspections.Sort();
            totalNumberOfInspections.Reverse();
            ulong monkeyBusiness = totalNumberOfInspections[0] * totalNumberOfInspections[1];

            Console.WriteLine($"Day 11, Part 2 Solution: {monkeyBusiness}");
        }

        private static Dictionary<string, Monkey> LoadMonkeys(string file)
        {
            ulong globalModulus = 1;
            Dictionary<string, Monkey> monkeys = new Dictionary<string, Monkey>();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day11/" + file))
            {
                string line;
                Dictionary<string, Regex> regexes = GetRegexes();
                Monkey currentMonkey = null;
                Match match;
                while ((line = reader.ReadLine()) != null)
                {
                    // This means we need to create a new monkey
                    match = regexes["id"].Match(line);
                    if (match.Success)
                    {
                        currentMonkey = new Monkey(match.Groups[1].Value);
                        monkeys.Add(match.Groups[1].Value, currentMonkey);
                    }
                    foreach (var pair in regexes)
                    {
                        match = regexes[pair.Key].Match(line);
                        if (match.Success)
                        {
                            InitialiseValue(match, pair.Key, currentMonkey);
                            break;
                        }
                    }
                }
            }
            foreach (var monkey in monkeys.Values)
            {
                globalModulus *= monkey.Divisor;
            }
            Monkey.GlobalModulus = globalModulus;
            return monkeys;
        }

        private static Dictionary<string, Regex> GetRegexes()
        {
            Dictionary<string, Regex> regexes = new Dictionary<string, Regex>
            {
                {"id", new Regex(@"Monkey (\d+)") },
                {"starting items", new Regex(@"Starting items: (.+)") },
                {"operator", new Regex(@"Operation: new = old (\+|-|\*|/) (\d+|old)") },
                {"divisor", new Regex(@"Test: divisible by (\d+)") },
                {"true monkey", new Regex(@"If true: throw to monkey (\d+)") },
                {"false monkey", new Regex(@"If false: throw to monkey (\d+)")}
            };
            return regexes;
        }

        private static void InitialiseValue(Match match, string key, Monkey monkey)
        {
            if (monkey is null)
                return;
            switch (key)
            {
                case "id":
                    monkey.Id = match.Groups[1].Value;
                    break;
                case "starting items":
                    var worryLevels = match.Groups[1].Value.ToString().Split(",").Select(x => Convert.ToUInt64(x)).ToList();
                    foreach (var worryLevel in worryLevels)
                        monkey.WorryLevels.Enqueue(worryLevel);
                    break;
                case "operator":
                    monkey.Operator = match.Groups[1].Value;
                    monkey.Increment = match.Groups[2].Value;
                    break;
                case "divisor":
                    monkey.Divisor = Convert.ToUInt64(match.Groups[1].Value);
                    break;
                case "true monkey":
                    monkey.TrueMonkey = match.Groups[1].Value;
                    break;
                case "false monkey":
                    monkey.FalseMonkey = match.Groups[1].Value;
                    break;
            }
        }
    }
}