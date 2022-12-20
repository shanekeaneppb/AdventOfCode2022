using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day16
{
    public partial class Day16Solutions
    {
        public static void Part1()
        {
            Dictionary<string, Valve> valves = LoadValves("test.txt");
            
            Dictionary<string, Dictionary<string, ValveInfo>> costs = GetValveCosts(valves);

            int totalTime = 30;
            string startValve = "AA";

            int maxPressure = GetMaxPressure(startValve, costs, totalTime);
            Console.WriteLine($"Day 16, Part 1 Solution: {maxPressure}");

        }
        public static void Part2()
        {
            Console.WriteLine($"Day 16, Part 2 Solution: ");
        }

        private static Dictionary<string, Valve> LoadValves(string file)
        {
            Dictionary<string, Valve> valves = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day16/" + file))
            {
                string line;
                Regex singleRegex = new(@"Valve ([A-Z][A-Z]) has flow rate=(\d+); tunnels lead to valves (.+)");
                Regex pluralRegex = new(@"Valve ([A-Z][A-Z]) has flow rate=(\d+); tunnel leads to valve (.+)");
                Match match;
                string valveName;
                string[] neighbourValves;
                int flowRate;
                Valve valve;
                while ((line = reader.ReadLine()) != null)
                {
                    match = singleRegex.Match(line.Trim());
                    if (!match.Success)
                    {
                        match = pluralRegex.Match(line.Trim());
                        if(!match.Success)
                            continue;
                    }
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
            return valves;
        }

        private static Dictionary<string, Dictionary<string, ValveInfo>> GetValveCosts(Dictionary<string, Valve> valves)
        {
            Dictionary<string, Dictionary<string, ValveInfo>> costs = new();

            foreach(var valve in valves.Values)
            {
                costs.Add(valve.Name, new());
                foreach (var name in valves.Keys)
                    costs[valve.Name].Add(name, new());
                Search(valve, costs);
            }
            return costs;
        }

        private static void Search(Valve start, Dictionary<string, Dictionary<string, ValveInfo>> costs)
        {
            Queue<Valve> toVisit = new();
            HashSet<Valve> visited = new();

            toVisit.Enqueue(start);
            costs[start.Name][start.Name].Distance = 0;

            Valve currentValve = null, previous;

            while (toVisit.Count > 0)
            {
                previous = (currentValve != null) ? currentValve : null;
                currentValve = toVisit.Dequeue();
                visited.Add(currentValve);
                foreach (var neighbour in currentValve.Neighbours)
                {
                    if (visited.Contains(neighbour))
                        continue;
                    costs[start.Name][neighbour.Name].Distance = costs[start.Name][currentValve.Name].Distance + 1;
                    costs[start.Name][neighbour.Name].FlowRate = neighbour.FlowRate;
                    if (!toVisit.Contains(neighbour))
                    { 
                        toVisit.Enqueue(neighbour);
                    }
                }
            }
        }

        private static int GetMaxPressure(string start, Dictionary<string, Dictionary<string, ValveInfo>>  costs, int totalTime)
        {
            int totalPressureReleased = 0, valueOfCurrentValve;
            string currentValve = start;
            int remainingTime = totalTime, timeCost = 0;
            HashSet<string> open = new HashSet<string>();
            Console.WriteLine($"{remainingTime} minutes remaining");
            int totalcost = 0;
            while (remainingTime > 0)
            {
                (currentValve, timeCost, valueOfCurrentValve) = GetMostEfficientValve(currentValve, costs, remainingTime, open);
                open.Add(currentValve);
                totalPressureReleased += valueOfCurrentValve;
                remainingTime -= timeCost;
                Console.WriteLine($"Open valve {currentValve}, costing {timeCost} and releasing a total of {valueOfCurrentValve}");
                Console.WriteLine($"{remainingTime} minutes remaining");
                totalcost += timeCost;
            }
            Console.WriteLine($"Total time cost = {totalcost}");
            return totalPressureReleased;
        }
        private static (string, int, int) GetMostEfficientValve(string start, Dictionary<string, Dictionary<string, ValveInfo>> costs, int remainingTime, HashSet<string> open)
        {
            string nextValveName, maxPressureValveName = "";
            int maxPressureValveValue = 0;

            int timeToValve = 0, valveValue;

            foreach(var pair in costs[start])
            {
                nextValveName = pair.Key;
                timeToValve = pair.Value.Distance + 1; // Add +1 as takes additional minute to open the valve
                if (open.Contains(nextValveName) || (remainingTime - timeToValve <= 0))
                    continue;
                valveValue = (remainingTime - timeToValve) * pair.Value.FlowRate;
                if(valveValue > maxPressureValveValue)
                {
                    maxPressureValveValue = valveValue;
                    maxPressureValveName = nextValveName;
                }

            }

            return (maxPressureValveName, timeToValve, maxPressureValveValue);
        }
    }
}