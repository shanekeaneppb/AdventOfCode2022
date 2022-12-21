using System.Text.RegularExpressions;

namespace AdventOfCode2022.Day13
{
    public class Day13Solutions
    {
        public static void Part1()
        {
            int sum = 0;
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day13/" + "input.txt"))
            {
                string line, nextLine;
                int i = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                        continue;
                    line = line.Trim();
                    nextLine = reader.ReadLine().Trim();
                    sum += i * Compare(line, nextLine);
                    i++;
                    //Console.WriteLine(line);
                }

                //Compare("11, 1, 1]", "11, 1, 1]");
            }
            Console.WriteLine($"Day 13, Part 1 Solution: {sum}");
        }
        public static void Part2()
        {
            List<string> packets = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day13/" + "input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                        continue;
                    line = line.Trim();
                    packets.Add(line);
                }
            }
            Sort(packets);
            int decoderKey = GetDecoderKey(packets);
            Console.WriteLine($"Day 13, Part 2 Solution: {decoderKey}");
        }

        private static int Compare(string left, string right)
        {
            int leftInt, rightInt;
            char l = left[0];
            char r = right[0];

            if(char.IsDigit(l) && char.IsDigit(r))
            {
                leftInt = GetInt(left); rightInt = GetInt(right);
                if (leftInt < rightInt)
                    return 1;
                else if (rightInt < leftInt)
                    return 0;
                else
                    return Compare(left.Substring(leftInt.ToString().Length), right.Substring(rightInt.ToString().Length));
            }
            if (l == r)
                return Compare(left.Substring(1), right.Substring(1));
            else if (l == '[' && char.IsDigit(r))
            {
                rightInt = GetInt(right);
                right = right.Insert(rightInt.ToString().Length, "]");
                return Compare(left.Substring(1), right);
            }
            else if (r == '[' && char.IsDigit(l))
            {
                leftInt = GetInt(left);
                left = left.Insert(leftInt.ToString().Length, "]");
                return Compare(left, right.Substring(1));
            }
            else if (l == ']' && r != ']')
                return 1;
            //Console.WriteLine($"Left = {left}");
            //Console.WriteLine($"Right = {right}");
            return 0;
        }

        private static bool AreBothNumbers(string left, string right)
        {
            Regex numRegex = new(@"(^\d+)");
            Match leftMatch, rightMatch;

            leftMatch = numRegex.Match(left);
            rightMatch = numRegex.Match(right);

            return (numRegex.IsMatch(left) && numRegex.IsMatch(right));
        }

        private static int GetInt(string str)
        {
            string intStr = "";
            foreach(char c in str)
            {
                if(!char.IsDigit(c))
                    break;
                intStr += c.ToString();
            }
            return Convert.ToInt32(intStr);
        }

        private static void Sort(List<string> packets)
        {
            string temp;
            for(int i = 0; i < packets.Count; i++)
            {
                for(int j = 0; j < packets.Count - 1; j++)
                {
                    if (Compare(packets[j], packets[j + 1]) == 1)
                        continue;
                    temp = packets[j];
                    packets[j] = packets[j+1];
                    packets[j+1] = temp;
                }
            }
        }
        private static int GetDecoderKey(List<string> packets)
        {
            int decoderKey = 1;
            for(int i = 0; i < packets.Count; i++)
            {
                if (packets[i] == "[[2]]" || packets[i] == "[[6]]")
                    decoderKey *= (i + 1);
            }
            return decoderKey;
        }

    }
}