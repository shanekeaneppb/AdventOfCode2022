using System.Text.RegularExpressions;


namespace AdventOfCode2022.Day4
{
    struct Range
    {
        public int Low;
        public int High;
    }
    public class Day4Solutions
    {
        public static void Part1()
        {
            int overlapCount = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day4/input.txt"))
            {
                string ln;
                MatchCollection matches;
                Regex regex = new Regex(@"(\d+)-(\d+),(\d+)-(\d+)");
                Range first = new Range();
                Range second= new Range();
                while ((ln = file.ReadLine()) != null)
                {
                    matches = regex.Matches(ln);
                    first.Low = Convert.ToInt32(matches[0].Groups[1].Value);
                    first.High = Convert.ToInt32(matches[0].Groups[2].Value);
                    second.Low = Convert.ToInt32(matches[0].Groups[3].Value);
                    second.High = Convert.ToInt32(matches[0].Groups[4].Value);
                    if(IsContained(first, second))
                        overlapCount++;
                }
            }
            Console.Write($"Day 4, Part 1 Solution: {overlapCount}");
        }
        public static void Part2()
        {
            int overlapCount = 0;
            using (StreamReader file = new StreamReader(@"../../../../AdventOfCode2022.Day4/input.txt"))
            {
                string ln;
                MatchCollection matches;
                Regex regex = new Regex(@"(\d+)-(\d+),(\d+)-(\d+)");
                Range first = new Range();
                Range second = new Range();
                while ((ln = file.ReadLine()) != null)
                {
                    matches = regex.Matches(ln);
                    first.Low = Convert.ToInt32(matches[0].Groups[1].Value);
                    first.High = Convert.ToInt32(matches[0].Groups[2].Value);
                    second.Low = Convert.ToInt32(matches[0].Groups[3].Value);
                    second.High = Convert.ToInt32(matches[0].Groups[4].Value);
                    if (IsOverlap(first, second))
                        overlapCount++;
                }
            }
            Console.Write($"Day 4, Part 2 Solution: {overlapCount}");
        }

        private static bool IsContained(Range first, Range second)
        {
            if((first.Low >= second.Low) && (first.High <= second.High))
                return true;
            if ((second.Low >= first.Low) && (second.High <= first.High))
                return true;
            return false;
        }
        private static bool IsOverlap(Range first, Range second)
        {
            if (((first.Low >= second.Low) && (first.Low <= second.High)) || ((first.High >= second.Low) && (first.High <= second.High)))
                return true;
            if (((second.Low >= first.Low) && (second.Low <= first.High)) || ((second.High >= first.Low) && (second.High <= first.High)))
               return true;
            return false;
        }

    }
}