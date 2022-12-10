using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day10
{
    public class Day10Solutions
    {
        public static void Part1()
        {
            Regex regex = new Regex(@"addx (-?\d+)");
            Match match;
            int i, signalIndex = 0, numberToAdd, stepsPerCycle = 2, x = 1, cycleCount = 0, signalStrength = 0;
            int[] signalIndices = { 20, 60, 100, 140, 180, 220 };
            int maxSignalIndex = signalIndices.Length;

            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day10/" + "input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if ((signalIndex < maxSignalIndex) && (cycleCount == signalIndices[signalIndex]))
                    {
                        signalStrength += cycleCount * x;
                        signalIndex++;
                    }
                    match = regex.Match(line);
                    if (!match.Success)
                    {
                        cycleCount++;
                        continue;
                    }
                    numberToAdd = Convert.ToInt32(match.Groups[1].Value);
                    for (i = 0; i < stepsPerCycle; i++)
                    {
                        cycleCount++;
                        if ((signalIndex < maxSignalIndex) && (cycleCount == signalIndices[signalIndex]))
                        {
                            signalStrength += cycleCount * x;
                            signalIndex++;
                        }
                    }
                    x += numberToAdd;
                }
            }
            Console.Write($"Day 10, Part 1 Solution: {signalStrength}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 10, Part 2 Solution: ");
            Regex regex = new Regex(@"addx (-?\d+)");
            Match match;
            int i, numberToAdd, stepsPerCycle = 2, x = 1, cycleCount = 0, rowSize = 40;

            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day10/" + "input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    match = regex.Match(line);
                    if (!match.Success)
                    {
                        DrawToScreen(x, cycleCount, rowSize);
                        cycleCount++;
                        continue;
                    }
                    numberToAdd = Convert.ToInt32(match.Groups[1].Value);
                    for (i = 0; i < stepsPerCycle; i++)
                    {
                        DrawToScreen(x, cycleCount, rowSize);
                        cycleCount++;
                    }
                    x += numberToAdd;
                }
                DrawToScreen(x, cycleCount, rowSize);
            }
        }

        private static void DrawToScreen(int x, int cycleCount, int rowSize)
        {
            cycleCount %= rowSize;
            if(cycleCount == 0)
                Console.WriteLine();
            if((cycleCount >= x - 1) && (cycleCount <= x + 1))
                Console.Write("#");
            else
                Console.Write(".");
        }
    }
}