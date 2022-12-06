namespace AdventOfCode2022.Day6
{
    public class Day6Solutions
    {
        public static void Part1()
        {
            int i = GetStartingIndex(4);
            Console.Write($"Day 6, Part 1 Solution: {i}");
        }
        public static void Part2()
        {
            int i = GetStartingIndex(14);
            Console.Write($"Day 6, Part 2 Solution: {i}");
        }

        private static int GetStartingIndex(int markerSize)
        {
            int i = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day6/input.txt"))
            {
                string ln;
                Queue<char> marker = new Queue<char>();

                while ((ln = file.ReadLine()) != null)
                {
                    i = 1;
                    foreach (char c in ln)
                    {
                        marker.Enqueue(c);

                        if (i >= markerSize)
                        {
                            if (marker.Distinct().Count() == marker.Count())
                                break;
                            marker.Dequeue();
                        }
                        i++;
                    }
                }
            }
            return i;
        }
    }
}