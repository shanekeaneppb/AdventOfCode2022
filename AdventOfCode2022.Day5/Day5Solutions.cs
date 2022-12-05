using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day5
{
    public class Day5Solutions
    {
        public static void Part1()
        {

            string solution = Solve(RearrangeCrates9000);
            Console.Write($"Day 5, Part 1 Solution: {solution}");
        }
        public static void Part2()
        {
            string solution = Solve(RearrangeCrates9001);
            Console.Write($"Day 5, Part 2 Solution: {solution}");
        }

        private static string Solve(Action<List<Stack<string>>, int, int, int> craneFunction)
        {
            List<string> stackStrings = new List<string>();
            List<Stack<string>> stacks;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day5/input.txt"))
            {
                string ln;
                int move, from, to;
                while ((ln = file.ReadLine()) != "")
                {
                    stackStrings.Add(ln);
                }
                stackStrings.RemoveAt(stackStrings.Count - 1);
                stacks = BuildStacks(stackStrings);
                Regex regex = new Regex(@"move (?<MOVE>\d+) from (?<FROM>\d+) to (?<TO>\d+)");
                while ((ln = file.ReadLine()) != null)
                {
                    var r = regex.Matches(ln);
                    move = Convert.ToInt32(r[0].Groups[1].Value);
                    from = Convert.ToInt32(r[0].Groups[2].Value) - 1;
                    to = Convert.ToInt32(r[0].Groups[3].Value) - 1;
                    craneFunction(stacks, move, from, to);
                }
            }
            StringBuilder solution = new StringBuilder();
            foreach (Stack<string> stack in stacks)
            {
                solution.Append(stack.Peek());
            }
            return solution.ToString();
        }

        private static List<Stack<string>> BuildStacks(List<string> stacks)
        {
            int stackSize = 4;
            int strLen = stacks[0].Length;
            string crateLabel;
            string currentRow;
            int i, j;

            List<Stack<string>> stacks2 = new List<Stack<string>>();
            for(i = 0; i < (strLen+1)/stackSize; i++)
            {
                stacks2.Add(new Stack<string>());
            }
            for(i = stacks.Count-1; i >= 0; i--)
            {
                currentRow = stacks[i];
                for (j = 0; j < strLen; j += stackSize)
                {
                    crateLabel = currentRow[j+1].ToString();
                    if (crateLabel == " ")
                        continue;
                    stacks2[j / stackSize].Push(crateLabel);
                }
               
            }
            return stacks2;
        }

        private static void RearrangeCrates9000(List<Stack<string>> stacks, int move, int from, int to)
        {
            for(int i = 0; i < move; i++)
            {
                stacks[to].Push(stacks[from].Pop());
            }
        }

        private static void RearrangeCrates9001(List<Stack<string>> stacks, int move, int from, int to)
        {
            Stack<string> tempStack = new Stack<string>();
            for (int i = 0; i < move; i++)
            {
                tempStack.Push(stacks[from].Pop());
            }
            for (int i = 0; i < move; i++)
            {
                stacks[to].Push(tempStack.Pop());
            }
        }
    }
}

