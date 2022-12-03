using System.Security;

namespace AdventOfCode2022.Day3
{
    public class Day3Solutions
    {
        public static void Part1()
        {
            int common_sum = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day3/input.txt"))
            {
                string ln;
                int length;
                while ((ln = file.ReadLine()) != null)
                {
                    length = ln.Length;
                    common_sum += GetCommonScore(ln, length);
                }
            }
            Console.WriteLine($"Day 3, Part 1 Solution: {common_sum}");
        }
        public static void Part2()
        {
            int common_sum = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day3/input_c.txt"))
            {
                int group_size = 3; int i;
                string[] groups = new string[group_size];
                int indexOfMin = 0;
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    groups[0] = ln;
                    for(i = 1; i < group_size; i++)
                    {
                        ln = file.ReadLine();
                        groups[i] = ln;
                        if(ln.Length < groups[indexOfMin].Length)
                        {
                            indexOfMin = i;
                        };
                    }
                    foreach(char c in groups[indexOfMin])
                    {
                        if(groups[(indexOfMin + 1) % group_size].Contains(c) 
                            && groups[(indexOfMin + 2) % group_size].Contains(c))
                        {
                            common_sum += GetCharValue(c);
                            break;
                        }
                    }
                }
            }
            Console.WriteLine($"Day 3, Part 2 Solution: {common_sum}");
        }

        private static int GetCommonScore(string str, int length)
        {
            int common_score = 0;
            int common_char = -1;
            int half_length = length / 2;
            string left_string = str.Substring(0, half_length);
            string right_string = str.Substring(half_length, half_length);
            foreach (char c in left_string)
            {
                if (right_string.Contains(c))
                {
                    common_char = (int)c;
                    break;
                }
            }
            common_score = GetCharValue(common_char);

            return common_score;
        }

        private static int GetCharValue(int c)
        {
            int value = -1;
            if (c >= 97 && c <= 122)
            {
                value = c - 96;
            }
            else if (c >= 65 && c <= 90)
            {

                value = c - 38;
            }
            return value;
        }
    }
}