using System.ComponentModel;

namespace AdventOfCode2022.Day14
{
    public class Day14Solutions
    {
        struct Wall
        {
            public int X;
            public int Y;

            public Wall(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public string ToString()
            {
                return $"({X}, {Y})";
            }

           
        }
        public static void Part1()
        {
            var walls = LoadWalls("input.txt");
            Wall sandSpawmn = new(500, 0);
            int unitsOfSand = SandFall(walls, sandSpawmn);
            Console.WriteLine($"Day 14, Part 1 Solution: {unitsOfSand}");
        }
        public static void Part2()
        {
            var walls = LoadWalls("input.txt");
            Wall sandSpawmn = new(500, 0);
            int unitsOfSand = SandFallWithFloor(walls, sandSpawmn);
            Console.WriteLine($"Day 14, Part 1 Solution: {unitsOfSand}");
        }

        private static HashSet<Wall> LoadWalls(string file)
        {
            HashSet<Wall> walls = new();
            using (StreamReader reader = new StreamReader(@"../../../../AdventOfCode2022.Day13/" + file))
            {
                string line;
                string[] coords;
                int i;
                while ((line = reader.ReadLine()) != null)
                {
                    coords = line.Split(" -> ");
                    for (i = 0; i < coords.Length - 1; i++)
                    {
                        AddPoints(walls, coords[i], coords[i + 1]);
                    }
                }
            }
            return walls;
        }

        private static void AddPoints(HashSet<Wall> points, string startCoord, string endCoord)
        {
            int i;
            var startCoordValues = startCoord.Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            var endCoordValues = endCoord.Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            if (startCoordValues[0] == endCoordValues[0])
            {
                for (i = Math.Min(startCoordValues[1], endCoordValues[1]); i <= Math.Max(startCoordValues[1], endCoordValues[1]); i++)
                {
                    points.Add(new Wall(startCoordValues[0], i));
                }
            }
            else if (startCoordValues[1] == endCoordValues[1])
            {
                for (i = Math.Min(startCoordValues[0], endCoordValues[0]); i <= Math.Max(startCoordValues[0], endCoordValues[0]); i++)
                {
                    points.Add(new Wall(i, startCoordValues[1]));
                }
            }
            else
                throw new InvalidEnumArgumentException();
        }

        private static bool MoveDown(ref Wall sand, HashSet<Wall> walls, int floor)
        {
            // Note moving "down" actually means increasing the y coordinate by the convention in the question
            Wall newPosition = new(sand.X, sand.Y + 1);
            if(walls.Contains(newPosition) || newPosition.Y >= floor)
                return false;
            sand = newPosition;
            return true;
        }

        private static bool MoveDownAndLeft(ref Wall sand, HashSet<Wall> walls, int floor)
        {
            // Note moving "down" actually means increasing the y coordinate by the convention in the question
            Wall newPosition = new(sand.X - 1, sand.Y + 1);
            if (walls.Contains(newPosition) || newPosition.Y >= floor)
                return false;
            sand = newPosition;
            return true;
        }
        private static bool MoveDownAndRight(ref Wall sand, HashSet<Wall> walls, int floor)
        {
            // Note moving "down" actually means increasing the y coordinate by the convention in the question
            Wall newPosition = new(sand.X + 1, sand.Y + 1);
            if (walls.Contains(newPosition) || newPosition.Y >= floor)
                return false;
            sand = newPosition;
            return true;
        }

        private static int SandFall(HashSet<Wall> walls, Wall sandSpawn)
        {
            int unitsOfSand = 0;
            int abyss = walls.Select(wall => wall.Y).Max();
            int floor = walls.Select(wall => wall.Y).Max() + 2;
            bool intoTheAbyss = false;
            Wall sand;
            while(true)
            {
                sand = new(sandSpawn.X, sandSpawn.Y);
                while (MoveDown(ref sand, walls, floor) || MoveDownAndLeft(ref sand, walls, floor) || MoveDownAndRight(ref sand, walls, floor))
                {
                    if (sand.Y >= abyss)
                    {
                        intoTheAbyss = true;
                        break;
                    }
                }
                if (intoTheAbyss)
                    break;
                walls.Add(sand);
                unitsOfSand++;
            }
            return unitsOfSand;
        }

        private static int SandFallWithFloor(HashSet<Wall> walls, Wall sandSpawn)
        {
            int unitsOfSand = 0;
            int floor = walls.Select(wall => wall.Y).Max() + 2;
            Wall sand;
            while (true)
            {
                sand = new(sandSpawn.X, sandSpawn.Y);
                while (MoveDown(ref sand, walls, floor) || MoveDownAndLeft(ref sand, walls, floor) || MoveDownAndRight(ref sand, walls, floor)) { }
                if ((sand.X == sandSpawn.X && (sand.Y == sandSpawn.Y)))
                    break;
                walls.Add(sand);
                unitsOfSand++;
            }
            return unitsOfSand + 1; // +1 as last sand (the one that blocks the source) is not counted 
        }
    }
}