using System.Collections.Generic;

namespace AdventOfCode2022.Day24
{
    public class Day24Solutions
    {
        public static void Part1()
        {
            (Point start, Point end, HashSet<Point> walls, HashSet<Point> blizzard) = LoadMap("test.txt");
            Console.WriteLine($"Day 24, Part 1 Solution: ");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 24, Part 2 Solution: ");
        }

        private static (Point, Point, HashSet<Point>, HashSet<Point> ) LoadMap(string file)
        {
            List<string> rows = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day24/" + file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    rows.Add(line);
                }
            }
            int row, column;
            Point start = null, end = null;
            HashSet<Point> walls = new();
            HashSet<Point> blizzard = new();
            char c;
            for(row = 0; row < rows.Count; row++)
            {
                for (column = 0; column < rows[row].Length; column++)
                {
                    c = rows[row][column];
                    if((row == 0) && (c == '.'))
                        start = new(row, column);
                    else if ((row == rows.Count - 1) && (c == '.')) 
                        end = new(row, column);
                    else if(c == '#')
                        walls.Add(new(row, column));
                    else
                        blizzard.Add(new(row, column, c));
                }
            }
            return (start, end, walls, blizzard);
        }
    }
}