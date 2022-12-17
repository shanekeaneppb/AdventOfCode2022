using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day16
{
    public class Day16Solutions
    {
        public static void Part1()
        {
            Dictionary<string, Valve> valves = new();
            Dictionary<string, Dictionary<string, int>> costs = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day16/" + "test.txt"))
            {
                string line;
                Regex regex = new(@"Valve ([A-Z][A-Z]) has flow rate=(\d+); tunnels lead to valves (.+)");
                Match match;
                string valveName;
                string[] neighbourValves;
                int flowRate;
                Valve valve;
                while ((line = reader.ReadLine()) != null)
                {
                    match = regex.Match(line.Trim());
                    if (!match.Success)
                        continue;

                    valveName = match.Groups[1].Value;
                    flowRate = Convert.ToInt32(match.Groups[2].Value);
                    neighbourValves = match.Groups[3].Value.Split(",");

                    if ((valves.ContainsKey(valveName)) && (valves[valveName].Name != null))
                    {
                        valve = valves[valveName];
                    }
                    else if ((valves.ContainsKey(valveName)) && (valves[valveName].Name == null))
                    {
                        valve = valves[valveName];
                        valve.Name = valveName;
                        valve.FlowRate = flowRate;
                    }  
                    else
                    {
                        valve = new Valve(valveName, flowRate);
                        valves.Add(valveName, valve);
                    }
                    valve.AddNeighbours(neighbourValves, valves);
                }
            }
            Console.WriteLine($"Day 16, Part 1 Solution: ");

        }
        public static void Part2()
        {
            Console.WriteLine($"Day 16, Part 2 Solution: ");
        }
    }
}