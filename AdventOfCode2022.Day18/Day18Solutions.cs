namespace AdventOfCode2022.Day18
{
    public partial class Day18Solutions
    {
        public static void Part1()
        {
            int exposedArea = 0;
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day18/" + "input.txt"))
            {
                string line;
                List<Cube> cubes = new();
                Cube cube;

                while ((line = reader.ReadLine()) != null)
                {
                    exposedArea += 6;
                    cube = new(line);
                    foreach (var c in cubes)
                        exposedArea -= 2*IsAdjacent(c, cube);
                    cubes.Add(cube);
                }
            }
            Console.WriteLine($"Day 18, Part 1 Solution: {exposedArea}");
        }
        public static void Part2()
        {
            Console.WriteLine($"Day 18, Part 2 Solution:");
        }

        private static int IsAdjacent(Cube c1, Cube c2)
        {
            if((c1.X == c2.X) && (c1.Y == c2.Y))
            {
                return ((c1.Z == (c2.Z + 1)) || (c1.Z == (c2.Z - 1))) ? 1: 0;
            }
            if ((c1.X == c2.X) && (c1.Z == c2.Z))
            {
                return ((c1.Y == (c2.Y + 1)) || (c1.Y == (c2.Y - 1))) ? 1 : 0;
            }
            if ((c1.Y == c2.Y) && (c1.Z == c2.Z))
            {
                return ((c1.X == (c2.X + 1)) || (c1.X == (c2.X - 1))) ? 1 : 0;
            }
            return 0;
        }
    }
}